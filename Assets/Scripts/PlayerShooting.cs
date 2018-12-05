using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour {

	[SerializeField] private GameObject _bulletPrefab;
	[SerializeField] private GameObject _gunpoint;

	[SerializeField] private float _firerate = 0.25f;
	private float _cooldown = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		HandleShooting();
	}

	void HandleShooting()
	{
		//count down
		_cooldown -= Time.deltaTime;

		//check if shooting
		if(Input.GetMouseButton(0)
		&& _cooldown <= 0)
		{
			_cooldown = _firerate;
			Instantiate(_bulletPrefab, _gunpoint.transform.position, _gunpoint.transform.rotation);
		}
	}
}
