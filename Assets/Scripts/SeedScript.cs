using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedScript : MonoBehaviour {

    private int _value = 1;

    public int Value
    {
        get
        {
            return _value;
        }
        set
        {
            _value = value;
        }
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0, 90 * Time.deltaTime, 0));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //TODO: add value to total seed counter

            Destroy(gameObject);
        }
    }

}
