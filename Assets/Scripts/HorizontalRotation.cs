using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalRotation : MonoBehaviour {

	[SerializeField] private Transform _mesh;

	void Update () {
		HandleRotationHorizontal();
	}

	void HandleRotationHorizontal()
	{
		float mouseMoveX = Input.GetAxis("Mouse X");
		transform.Rotate(new Vector3(0,1,0) * 4.0f * mouseMoveX);
		_mesh.Rotate(new Vector3(0,1,0)* 4.0f * mouseMoveX);
	}
}
