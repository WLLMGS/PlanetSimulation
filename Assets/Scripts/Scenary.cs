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

        //ray cast to the planet & where the ray hits is the new position of the tree
        //also rotate the tree correctly
		if(Physics.Raycast(transform.position, dir, out hit, 500))
		{
			//if(hit.collider.tag == "Planet") Debug.Log("HIT");
			transform.position = hit.point;	
			Vector3 updir = transform.up;
			Vector3 rotdir = (transform.position - attr.gameObject.transform.position).normalized;

			transform.rotation =  Quaternion.FromToRotation(updir, rotdir) * transform.rotation;	
		}

	}
	
	
}
