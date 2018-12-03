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


	private float _gravity = -50f;

	public void Attract(Transform other, float rotOffset, float modifier)
	{
		Vector3 dir = other.position - transform.position;
		Vector3 updir = other.up;

		dir.Normalize();

		other.GetComponent<Rigidbody>().AddForce(_gravity * dir * modifier);

		other.rotation = Quaternion.FromToRotation(updir, dir) * other.rotation * Quaternion.Euler(new Vector3(rotOffset, 0, 0));
	}

}
