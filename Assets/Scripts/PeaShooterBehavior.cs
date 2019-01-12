using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class PeaShooterBehavior : MonoBehaviour
{
    [SerializeField] private GameObject _bulletPrefab;
    private Transform _gunpoint;
    private Transform _factory;

    private Animator _animator;
    private Transform _target;
    private EnemyManager _enemyManager;
    private bool _canAttack = true;
    private float _attackCooldown = 0.75f;

    private static int _cost = 10;

    public static int Cost
    {
        get { return _cost; }
    }

    private SelectorNode _rootNode;

    private void Start()
    {
        PlantManager.Instance.RegisterPlant3(gameObject);

        _enemyManager = EnemyManager.Instance;
        _animator = GetComponentInChildren<Animator>();
        _animator.speed = 4.0f;

        _gunpoint = transform.Find("gunpoint");

        if (GameplayManager.Instance.CurrentFactory != null)
        {
            _factory = GameplayManager.Instance.CurrentFactory.transform;
        }

        //behavior tree
        _rootNode = new SelectorNode(
            //handle attack
            new SequenceNode(
                new ConditionNode(HasTarget),
                new ConditionNode(CanAttack),
                new ActionNode(RotateTowardsTarget),
                new ActionNode(DoAttack)
                ),
            //default action
            new SequenceNode(
                new ConditionNode(CanAttack),
                new ActionNode(RotateTowardsTarget)
                )
            );

    }

    private void Update()
    {
        //determine target
        DetermineTarget();
        //walk through behaviortree
        _rootNode.Tick();
    }
    //determine target -> get closest enemy entity
    private void DetermineTarget()
    {
        GameObject t = _enemyManager.GetClosestEnemy(transform);

        if (t != null) _target = t.transform;
        else _target = _factory;
    }

    //do attack
    private NodeState DoAttack()
    {

        //shoot
        Instantiate(_bulletPrefab, _gunpoint.position, _gunpoint.rotation);
        //play animation
        if (_animator == null) return NodeState.Failure;
        _animator.SetTrigger("Attack");

        //start cooldown
        _canAttack = false;
        Invoke("ReadyAttack", _attackCooldown);

        return NodeState.Success;
    }
    //rotate towards target
    private NodeState RotateTowardsTarget()
    {
        if (_target == null) return NodeState.Failure;

        Vector3 targetDir = _target.position - transform.position;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, 90.0f * Time.deltaTime, 0.0f);

        transform.rotation = Quaternion.LookRotation(newDir, transform.up);

        return NodeState.Success;
    }
    //check if has target
    private bool HasTarget()
    {
        return (_target != null);
    }
    //check if plant can attack
    private bool CanAttack()
    {
        return (_canAttack);
    }
    //check if ready to attack
    private void ReadyAttack()
    {
        _canAttack = true;
    }
}
