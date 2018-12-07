using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;

public class EnemyBehavior : MonoBehaviour {

    [SerializeField] private Transform _target;
    private float _distanceToShoot = 5.0f;

    SelectorNode _rootNode;
    bool _closeEnoughToPlayer = false;

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
            //Check if enemy is close to player -> if true shoot
            new SequenceNode(
                new ConditionNode(IsPlayerInRange),
                new ActionNode(RotateTowardsPlayer),
                new ActionNode(Shoot)
                ),
            //by default walk towards player
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
        return NodeState.Success;
    }

    private bool IsPlayerInRange()
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
