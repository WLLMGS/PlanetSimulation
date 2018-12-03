using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scenary : MonoBehaviour {


	// Use this for initialization
	void Start () {
		PlanetAttractorScript attr = PlanetAttractorScript.Instance;

		Vector3 dir =  attr.gameObject.transform.position - transform.position;
		dir.Normalize();
		RaycastHit hit;

		if(Physics.Raycast(transform.position, dir, out hit, 500))
		{
			//if(hit.collider.tag == "Planet") Debug.Log("HIT");
			transform.position = hit.point;	
			Vector3 updir = transform.up;
			Vector3 rotdir = (transform.position - attr.gameObject.transform.position).normalized;

			transform.rotation =  Quaternion.FromToRotation(updir, rotdir) * transform.rotation;	
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
