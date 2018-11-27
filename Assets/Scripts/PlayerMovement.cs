using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody _rb;
    private PlanetAttractorScript _attractor;

    // Use this for initialization
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _attractor = PlanetAttractorScript.Instance;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _attractor.Attract(transform);

        HandleMovement();
    }

    void HandleMovement()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _rb.AddForce(transform.up * 1.0f, ForceMode.Impulse);
        }

        float yaxis = Input.GetAxis("Vertical");
        float xaxis = Input.GetAxis("Horizontal");

        _rb.AddForce(yaxis * transform.forward * 20.0f);
        _rb.AddForce(xaxis * transform.right * 20.0f);
    }
}
