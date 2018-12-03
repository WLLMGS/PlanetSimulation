using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour {

	[SerializeField] private GameObject _bulletPrefab;
	[SerializeField] private GameObject _gunpoint;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		HandleShooting();
	}

	void HandleShooting()
	{
		if(Input.GetMouseButtonDown(0))
		{
			Instantiate(_bulletPrefab, _gunpoint.transform.position, _gunpoint.transform.rotation);
		}
	}
}
