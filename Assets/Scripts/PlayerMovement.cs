using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private static bool _CanMove = true;

    public static bool CanMove
    {
        get { return _CanMove; }
        set { _CanMove = value; }
    }

    private Rigidbody _rb;
    private float _moveforce = 10.0f;
    private float _maxspeed = 15.0f;
    

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked; //lock cursor in window, unlocked by pressing escape
    }

    void FixedUpdate()
    {
        if(_CanMove) HandleMovement();
    }

    void HandleMovement()
    {
        float yaxis = Input.GetAxis("Vertical");
        float xaxis = Input.GetAxis("Horizontal");

        Vector3 forward  = gameObject.transform.Find("Mesh").transform.forward;
        Vector3 right = gameObject.transform.Find("Mesh").transform.right;
        //  forward.x -= Mathf.Cos(10 * Mathf.Deg2Rad);

        //_rb.AddForce(yaxis * forward * _moveforce);
        //_rb.AddForce(xaxis * right * _moveforce);

        ////clamp velocity
        //if(_rb.velocity.magnitude > _maxspeed)
        //{
        //    _rb.velocity = _rb.velocity.normalized * _maxspeed;
        //}

        Vector3 velocity = Vector3.zero;
        velocity += yaxis * forward * _moveforce;
        velocity += xaxis * right * _moveforce;

        _rb.velocity = velocity;
    }

}
