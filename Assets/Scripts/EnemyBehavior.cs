using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class EnemyBehavior : MonoBehaviour {

    [SerializeField] private Transform _target;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _gunpoint;

    private float _firerate = 0.5f;
    private bool _canshoot = true;

    private float _distanceToShoot = 10.0f;
    private float _distanceToStop = 5.0f;

    SelectorNode _rootNode;
    bool _closeEnoughToPlayer = false;
    bool _isInRangeToShoot = false;

    public Transform Target
    {
        get
        {
            return _target;
        }
        set
        {
            _target = value;
        }
    }

    // Use this for initialization
    void Start()
    {
        //behavior tree
        _rootNode = new SelectorNode(
            //shoot and stand still when player is close enough
            new SequenceNode(
                new ConditionNode(IsInShootingRange),
                new ConditionNode(IsCloseEnoughToStop),
                 new ActionNode(RotateTowardsPlayer),
                new ActionNode(Shoot)
                ),

            //shoot and move when player is in range
            new SequenceNode(
                new ConditionNode(IsInShootingRange),
                new ActionNode(RotateTowardsPlayer),
                new ActionNode(Shoot),
                new ActionNode(NavigateTowardsPlayer)
                ),
            //just move if player is not in range to shoot
            new SequenceNode(
                new ActionNode(RotateTowardsPlayer),
                new ActionNode(NavigateTowardsPlayer)
                )
            );
    }
    private NodeState NavigateTowardsPlayer()
    {
        if (_target)
        {
            Vector3 distance = Vector3.MoveTowards(transform.position, _target.position, 3.0f * Time.deltaTime);
            transform.position = distance;
        }
        return NodeState.Success;
    }
    private NodeState RotateTowardsPlayer()
    {
        Vector3 targetDir = _target.position - transform.position;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, 90.0f * Time.deltaTime, 0.0f);
        Debug.DrawRay(transform.position, newDir, Color.red);

        transform.rotation = Quaternion.LookRotation(newDir, transform.up);

        return NodeState.Success;
    }

    private NodeState Shoot()
    {
        if(_canshoot)
        {
            _canshoot = false;
            StartCoroutine(GunCooldown());
            Instantiate(_bulletPrefab, _gunpoint.position, _gunpoint.rotation);
        }
            return NodeState.Success;
    }

    private bool IsCloseEnoughToStop()
    {
        float d = Vector3.Distance(transform.position, _target.position);
        return (d <= _distanceToStop);
    }

    private IEnumerator GunCooldown()
    {
        yield return new WaitForSeconds(_firerate);
        _canshoot = true;
    }

    private bool IsInShootingRange()
    {
       float d = Vector3.Distance(_target.position, transform.position);
       return (d <= _distanceToShoot);
    }

    // Update is called once per frame
    void Update()
    {
       _rootNode.Tick();
    }
}
