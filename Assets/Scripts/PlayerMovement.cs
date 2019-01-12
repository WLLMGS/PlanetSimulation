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
    
    private void Awake()
    {
        //dont destroy player on load
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked; //lock cursor in window, unlocked by pressing escape
        Cursor.visible = false;
    }

    void FixedUpdate()
    {
        //if player can move handle the movement
        if(_CanMove) HandleMovement();
    }
    //handle movement
    void HandleMovement()
    {
        float yaxis = Input.GetAxis("Vertical");
        float xaxis = Input.GetAxis("Horizontal");

        Vector3 forward  = gameObject.transform.Find("Mesh").transform.forward;
        Vector3 right = gameObject.transform.Find("Mesh").transform.right;
        
        Vector3 velocity = Vector3.zero;
        velocity += yaxis * forward * _moveforce;
        velocity += xaxis * right * _moveforce;

        _rb.velocity = velocity;
    }

}
