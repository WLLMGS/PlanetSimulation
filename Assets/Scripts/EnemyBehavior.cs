using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BehaviorTree;
using UnityEngine.UI;

public class EnemyBehavior : MonoBehaviour {

    [SerializeField] private Transform _player;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _gunpoint;


    private Animator _animator;

    private float _firerate = 1.0f;
    private bool _canshoot = true;

    private float _distanceToShoot = 10.0f;
    private float _distanceToStop = 5.0f;
    private float _PlayerHitCooldown = 1.0f;

    private SelectorNode _rootNode;
    private bool _closeEnoughToPlayer = false;
    private bool _isInRangeToShoot = false;
    private bool _isDead = false;
    private bool _isHitByPlayer = false;


    private PlantManager _plantManager;
    private Transform _target;


    public bool IsHitByPlayer
    {
        set
        {
            StartCoroutine(PlayerHitCooldown());
            _isHitByPlayer = true;
        }
    }

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

    // Use this for initialization
    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _plantManager = PlantManager.Instance;

        //behavior tree
        _rootNode = new SelectorNode(
            //when enemy dies
            new SequenceNode(
                new ConditionNode(IsDeadCond),
                new ActionNode(DeathAction)
                ),
            ////shoot and stand still when target is close enough
            //new SequenceNode(
            //    new ConditionNode(IsInShootingRange),
            //    new ConditionNode(IsCloseEnoughToStop),
            //    new ActionNode(RotateTowardsTarget),
            //    new ActionNode(Shoot)
            //    ),
            //shoot and move when target is in range
            new SequenceNode(
                new ConditionNode(IsInShootingRange),
                new ActionNode(RotateTowardsTarget),
                new ActionNode(Shoot),
                new ActionNode(NavigateTowardsTarget)
                ),
            //just move if target is not in range to shoot
            new SequenceNode(
                new ActionNode(RotateTowardsTarget),
                new ActionNode(NavigateTowardsTarget)
                )
            );
    }

    //=============== AI ===============
    private NodeState NavigateTowardsTarget()
    {
        if (_target)
        {
            Vector3 distance = Vector3.MoveTowards(transform.position, _target.position, 3.0f * Time.deltaTime);
            transform.position = distance;
        }
        return NodeState.Success;
    }
    private NodeState RotateTowardsTarget()
    {
        Vector3 targetDir = _target.position - transform.position;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, 90.0f * Time.deltaTime, 0.0f);
       
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
    
    private NodeState DeathAction()
    {
       // if (_animator == null) return NodeState.Failure;

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

    private bool IsDeadCond()
    {
        return _isDead;
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

    private IEnumerator Kill()
    {
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }

    private bool IsInShootingRange()
    {
       float d = Vector3.Distance(_target.position, transform.position);
       return (d <= _distanceToShoot);
    }

    private void DetermineTarget()
    {
        if(_isHitByPlayer)
        {
            _target = _player;
            return;
        }


        Transform closestPlant = _plantManager.GetClosestPlant(transform);

        if(closestPlant == null)
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

    private IEnumerator PlayerHitCooldown()
    {
        yield return new WaitForSeconds(_PlayerHitCooldown);
        _isHitByPlayer = false;
    }
    // Update is called once per frame
    void Update()
    {
        DetermineTarget();
       _rootNode.Tick();
    }
}
