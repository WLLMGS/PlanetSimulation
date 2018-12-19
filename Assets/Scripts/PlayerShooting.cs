using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{

    private static bool _CanShoot = true;

    public static bool CanShoot
    {
        get { return _CanShoot; }
        set { _CanShoot = value; }
    }

    [SerializeField] private GameObject _bulletPrefab;
	[SerializeField] private GameObject _gunpoint;

	[SerializeField] private float _firerate = 0.35f;
	private float _cooldown = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(_CanShoot) HandleShooting();
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
