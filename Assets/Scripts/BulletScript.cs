using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

	
	[SerializeField] private float _speed = 50.0f;

	void Start()
	{
		Invoke("Kill", 0.5f);
	}

	void Kill()
	{
		Destroy(gameObject);
	}

	void Update () {
		transform.position += transform.right * _speed * Time.deltaTime;	
	}

	void OnTriggerEnter(Collider other)
	{
		if(other.tag == "Enemy") Destroy(gameObject);
	}
}
