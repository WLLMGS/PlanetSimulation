using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour {

    [SerializeField] private Transform _target;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (_target)
        {
            Vector3 distance = Vector3.MoveTowards(transform.position, _target.position, 3.0f * Time.deltaTime);
            transform.position = distance;

            Vector3 targetDir = _target.position - transform.position;
            Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, 90.0f * Time.deltaTime, 0.0f);
            Debug.DrawRay(transform.position, newDir, Color.red);

            transform.rotation = Quaternion.LookRotation(newDir, transform.up);
        }

        

        

    }
}
