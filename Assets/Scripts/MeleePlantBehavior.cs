using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using System.Linq;

public class MeleePlantBehavior : MonoBehaviour
{

    [SerializeField] private Animator _animator;
    private static int _cost = 5;


    private SelectorNode rootNode;
    private List<GameObject> _enemiesInRange = new List<GameObject>();
    private bool _canAttack = true;
    private float _attackRate = 0.5f;
    GameObject _target = null;
    
    public static int Cost
    {
        get
        {
            return _cost;
        }
    }


    // Use this for initialization
    void Start()
    {
        //register the plant at the plant manager
        PlantManager.Instance.RegisterPlant1(transform.parent.gameObject);

        //behavior tree
        rootNode = new SelectorNode(
            new SequenceNode(
                new ConditionNode(HasEnemyInRange),
                new ConditionNode(CanAttack),
                new ActionNode(AttackClosestEnemy),
                new ActionNode(RotateTowardsTarget)
                )
            );
    }

    // Update is called once per frame
    void Update()
    {
        rootNode.Tick();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            _enemiesInRange.Add(other.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            _enemiesInRange.Remove(other.gameObject);
        }
    }

    //============== AI STUFF ==============

    private IEnumerator AttackCooldown()
    {
        yield return new WaitForSeconds(_attackRate);
        _canAttack = true;
    }

    //check if enemies are in range
    private bool HasEnemyInRange()
    {
        return (_enemiesInRange.Count > 0);
    }

    //get closest enemy in range
    private GameObject GetClosestEnemy()
    {
        GameObject closestEnemy = null;
        float closestDistance = 1000.0f;

        //check for empty first
        _enemiesInRange = _enemiesInRange.Where(item => item != null).ToList();

        //get closest enemy
        foreach (GameObject en in _enemiesInRange)
        {
            float distance = Vector3.Distance(transform.position, en.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestEnemy = en;
            }
        }

        return closestEnemy;
    }
    //attack that enemy
    private NodeState AttackClosestEnemy()
    {
        _target = GetClosestEnemy();

        if (_target == null) return NodeState.Failure;

        if (_animator == null) return NodeState.Failure;

        _animator.SetTrigger("Attack");

        HealthScript health = _target.GetComponent<HealthScript>();

        health.Damage(PlayerStats.PlayerDamage);

        _canAttack = false;

        StartCoroutine(AttackCooldown());


        return NodeState.Success;
    }

    private bool CanAttack()
    {
        return _canAttack;
    }

    private NodeState RotateTowardsTarget()
    {
        
        Vector3 targetDir = _target.transform.position - transform.position;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, 90.0f * Time.deltaTime, 0.0f);
        Debug.DrawRay(transform.position, newDir, Color.red);

        transform.parent.rotation = Quaternion.LookRotation(newDir, transform.up);

        return NodeState.Success;
    }
}
