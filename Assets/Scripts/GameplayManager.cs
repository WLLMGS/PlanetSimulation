using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour {

	[SerializeField] private GameObject _canvas;

	// Use this for initialization
	void Start () {
		Instantiate(_canvas, transform.position, Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
