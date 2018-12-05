using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyScript : MonoBehaviour {

    [SerializeField] private float _TTL = 2;

	void Start () {
        Invoke("DestroyGameObject", _TTL);
	}
	
	void DestroyGameObject()
    {
        Destroy(gameObject);
    }
}
