using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using UnityEngine.UI;

public class MeleeEnemyBehavior : MonoBehaviour
{
    [SerializeField] private Transform _player;
    private Transform _target;
    private Animator _animator;

    private float _movespeed = 4.0f;
    private float _stopDistance = 3.0f;
    private float _attackCooldown = 0.5f;
    private float _approachRange = 20.0f;

    private bool _canAttack = true;

    private bool _isDead = false;
    private bool _isHitByPlayer = false;

    private PlantManager _plantManager;

    private SelectorNode _rootNode;
    private EnemyStats _stats;

    public Transform Target
    {
        get
        {
            return _player;
        }
        set
        {
            _player = value;
        }
    }

    public bool IsDead
    {
        get
        {
            return _isDead;
        }
        set
        {
            _isDead = value;
        }
    }

    private void Start()
    {
        //register in enemy manager
        EnemyManager.Instance.RegisterEnemy(gameObject);

        _plantManager = PlantManager.Instance;

        var enemyStats = GetComponent<EnemyStats>();

        _approachRange = enemyStats.ApproachRange;
        _player = enemyStats.Player;
        _target = _player;

        _animator = GetComponentInChildren<Animator>();
        _stats = GetComponent<EnemyStats>();

        //behavior tree
        _rootNode = new SelectorNode(
            //handle death
            new SequenceNode(
                new ConditionNode(IsDeadCond),
                new ActionNode(DeathAction)
                ),
            //do attack
            new SequenceNode(
                new ConditionNode(CanAttack),
                new ConditionNode(IsCloseEnough),
                new ActionNode(DoAttack)
                ),
            //default action
            new SequenceNode(
                new ConditionNode(IsTargetInRange),
                new ActionNode(NavigateTowardsTarget),
                new ActionNode(RotateTowardsTarget)
                )
            );
    }

    private void Update()
    {
        //determine target
        DetermineTarget();
        //go through behavior tree
        _rootNode.Tick();
    }

    //determine target
    //get closest friendly entity
    private void DetermineTarget()
    {
        if (_isHitByPlayer)
        {
            _target = _player;
            return;
        }


        Transform closestPlant = _plantManager.GetClosestPlant(transform);

        if (closestPlant == null)
        {
            _target = _player;
            return;
        }

        float d1 = Vector3.Distance(closestPlant.position, transform.position);
        float d2 = Vector3.Distance(_player.position, transform.position);

        if (d1 < d2)
        {
            _target = closestPlant;
        }
        else _target = _player;

    }

    //============= AI ============= 
    //move towards target
    private NodeState NavigateTowardsTarget()
    {
        if (_target)
        {
            Vector3 distance = Vector3.MoveTowards(transform.position, _target.position, _movespeed * Time.deltaTime);
            transform.position = distance;
        }
        return NodeState.Success;
    }
    //rotate towards the target over the up axis
    private NodeState RotateTowardsTarget()
    {
        Vector3 targetDir = _target.position - transform.position;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, 90.0f * Time.deltaTime, 0.0f);

        transform.rotation = Quaternion.LookRotation(newDir, transform.up);

        return NodeState.Success;
    }

    private NodeState DoAttack()
    {
        //do melee attack animation
        _animator.SetTrigger("MeleeAttack");

        //do damage to target
        HealthScript health = _target.gameObject.GetComponent<HealthScript>();
        if (health == null) return NodeState.Failure;

        health.Damage(_stats.EnemyDamage);

        //start cooldown
        _canAttack = false;
        Invoke("ReadyAttack", _attackCooldown);

        //return success
        return NodeState.Success;
    }

    private NodeState DeathAction()
    {
        _animator.SetTrigger("Die");

        //disable health bar
        var slider = GetComponentInChildren<Slider>();
        if (slider == null) return NodeState.Success;
        slider.gameObject.SetActive(false);

        //disable capsule collider
        var cap = GetComponent<CapsuleCollider>();
        //if (cap == null) return NodeState.Failure;
        cap.enabled = false;

        //disable gravity body
        var grav = GetComponent<GravityBody>();
        //if (grav == null) return NodeState.Failure;
        grav.enabled = false;
        //destroy enemy when animation is complete
        StartCoroutine(Kill());

        return NodeState.Success;
    }

    //check if target is close enough to attack
    private bool IsCloseEnough()
    {
        if (_target == null) return false;

        float distance = Vector3.Distance(transform.position, _target.position);
        return (distance <= _stopDistance);
    }
    //check if enemy can attack
    private bool CanAttack()
    {
        return _canAttack;
    }
    //check if enemy is ready to attack
    private void ReadyAttack()
    {
        _canAttack = true;
    }
    //check if enemy is dead
    private bool IsDeadCond()
    {
        return _isDead;
    }
    //check if the target is in range
    private bool IsTargetInRange()
    {
        float distance = Vector3.Distance(transform.position, _target.position);
        return (distance <= _approachRange);
    }
    //destroy enemy after 1.5s
    private IEnumerator Kill()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
