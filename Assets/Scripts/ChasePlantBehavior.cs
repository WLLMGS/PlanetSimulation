using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;



public class ChasePlantBehavior : MonoBehaviour
{
    //cost
    private static int _cost = 7;

    public static int Cost
    {
        get { return _cost; }
    }

    //general
    private SelectorNode _rootNode;

    private EnemyManager _enemyManager;

    private Transform _factory = null;
    private Transform _target = null;

    private Animator _animator = null;

    private float _meleeRange = 2.0f;
    private bool _canAttack = true;
    private float _attackCooldown = 0.5f;
    private float _moveSpeed = 2.0f;

    private bool _IsCollidingWithFactory = false;

    private void Start()
    {
        //register the plant
        PlantManager.Instance.RegisterPlant2(gameObject);
        //get animator
        _animator = GetComponentInChildren<Animator>();
        //get enemy manager
        _enemyManager = EnemyManager.Instance;
        //if there is a factory currently get the transform
        if (GameplayManager.Instance.CurrentFactory != null)
        {
            _factory = GameplayManager.Instance.CurrentFactory.transform;
         }
        //set the current target to factory 
        _target = _factory;

        //behavior tree
        _rootNode = new SelectorNode(
            //attack node
            new SelectorNode(
                //if target & enemy in range & can attack ->attack
                new SequenceNode(
                    new ConditionNode(HasTarget),
                    new ConditionNode(CanAttack),
                    new ConditionNode(IsInMeleeRange),
                    new ActionNode(RotateTowardsTarget),
                    new ActionNode(Attack)
                    ),
                //if target, enemy in range but not ready to attack -> wait
                new SequenceNode(
                    new ConditionNode(HasTarget),
                    new ConditionNode(IsInMeleeRange),
                    new ActionNode(RotateTowardsTarget),
                    new ActionNode(DoNothing)
                    )
                ),
            //seek node
            new SequenceNode(
                new ConditionNode(HasTarget),
                new ActionNode(RotateTowardsTarget),
                new ActionNode(NavigateTowardsTarget)
                ),
            //default node
            new ActionNode(DoNothing)
            );
    }

    private void Update()
    {
        //calculate closest target
        CalculateClosestTarget();

        //tick behavior tree
        _rootNode.Tick();
    }

    private void CalculateClosestTarget()
    {
        GameObject closestEnemy = _enemyManager.GetClosestEnemy(transform);

        //if no enemies but factory still exists
        if (closestEnemy == null
            && _factory != null)
        {
            Debug.Log("CASE 1");
            _target = _factory;
            return;
        }
        //if no enemies & factory is destroyed -> target = null
        else if (closestEnemy == null && _factory == null)
        {
            _target = null;
            return;
        }
        else if (_factory == null
                 && closestEnemy != null)
        {
            _target = closestEnemy.transform;
            return;
        }


        //calculate closest
        float distanceFactory = Vector3.Distance(transform.position, _factory.position);
        float distanceEnemy = Vector3.Distance(transform.position, closestEnemy.transform.position);

        if (distanceEnemy >= distanceFactory)
        {
            _target = _factory;
            return;
        }
        else
        {
            _target = closestEnemy.transform;
            return;
        }

    }

    //=============== AI STUFF ===============
    private bool HasTarget()
    {
        return (_target != null);
    }

    //check if the target is in melee range
    private bool IsInMeleeRange()
    {
        if (_target != _factory)
        {
            float distance = Vector3.Distance(transform.position, _target.position);
            return (distance <= _meleeRange);
        }
        else
        {
            return (_IsCollidingWithFactory);
        }
       
    }
    //check if plant can attack
    private bool CanAttack()
    {   
        return _canAttack;
    }

    //rotate towards current target
    private NodeState RotateTowardsTarget()
    {

        Vector3 targetDir = _target.transform.position - transform.position;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, 90.0f * Time.deltaTime, 0.0f);
        //Debug.DrawRay(transform.position, newDir, Color.red);

        transform.rotation = Quaternion.LookRotation(newDir, transform.up);

        return NodeState.Success;
    }

    //do attack
    private NodeState Attack()
    {
        //request health script
        //if script does not exist -> return failure
        //do plant damage to target
        var health = _target.gameObject.GetComponent<HealthScript>();
        if (health == null) return NodeState.Failure;
        health.Damage(PlayerStats.PlantDamage);

        //play animation
        _animator.SetTrigger("Attack");

        //ready to attack -> false, invoke cooldown
        _canAttack = false;
        Invoke("ReadyAttack", _attackCooldown);

        return NodeState.Success;
    }
    //move towards the current target
    private NodeState NavigateTowardsTarget()
    {
        if (_target)
        {
            Vector3 distance = Vector3.MoveTowards(transform.position, _target.position, _moveSpeed * Time.deltaTime);
            transform.position = distance;
        }
        return NodeState.Success;
    }

    private NodeState DoNothing()
    {
        return NodeState.Success;
    }

    //check if plant is ready to attack
    private void ReadyAttack()
    {
        _canAttack = true;
    }

    //check if colliding with factory
    //if true set bool to true
    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.gameObject.tag == "Factory")
        {
            _IsCollidingWithFactory = true;
        }
    }

    //check if collision exit with factory
    //if true set bool to false
    private void OnCollisionExit(Collision other)
    {
        if (other.collider.gameObject.tag == "Factory")
        {
            _IsCollidingWithFactory = false;
        }
    }
}
