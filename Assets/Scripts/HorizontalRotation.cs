using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalRotation : MonoBehaviour
{

    private static bool _canRotate = true;

    public static bool CanRotate
    {
        get { return _canRotate; }
        set { _canRotate = value; }
    }

	[SerializeField] private Transform _mesh;

	void Update () {

        if(_canRotate) HandleRotationHorizontal();
	}

	void HandleRotationHorizontal()
	{
		float mouseMoveX = Input.GetAxis("Mouse X");
		transform.Rotate(new Vector3(0,1,0) * 4.0f * mouseMoveX);
		_mesh.Rotate(new Vector3(0,1,0)* 4.0f * mouseMoveX);
	}
}
