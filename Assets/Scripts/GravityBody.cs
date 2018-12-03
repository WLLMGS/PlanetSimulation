using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class GravityBody : MonoBehaviour {

	[SerializeField] private float _gravityModifier = 1.0f;

	private PlanetAttractorScript _attractor;
	private float _rotOffset = 0;

	public float RotationOffset
	{
		get{
			return _rotOffset;
		}

		set
		{
			_rotOffset = value;
		}
	}

	// Use this for initialization
	void Start () {
		_attractor = PlanetAttractorScript.Instance;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		_attractor.Attract(transform, _rotOffset, _gravityModifier);
	}
}
