using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetAttractorScript : MonoBehaviour
{
	private static PlanetAttractorScript _instance = null;

	public static PlanetAttractorScript Instance
	{
		get{
			return _instance;
		}
	}


	void Awake()
	{
		if(_instance == null) _instance = this;
	}


	private float _gravity = -20f;

	public void Attract(Transform other)
	{
		Vector3 dir = other.position - transform.position;
		Vector3 updir = other.up;

		dir.Normalize();

		other.GetComponent<Rigidbody>().AddForce(_gravity * dir);

		other.rotation = Quaternion.FromToRotation(updir, dir) * other.rotation;
	}

}
