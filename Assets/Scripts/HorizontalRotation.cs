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

        //handle rotation if can rotate is true
        if(_canRotate) HandleRotationHorizontal();
	}

	void HandleRotationHorizontal()
	{
        //use mouse horizontal movement to rotate the player
		float mouseMoveX = Input.GetAxis("Mouse X");
		transform.Rotate(new Vector3(0,1,0) * 4.0f * mouseMoveX);
		_mesh.Rotate(new Vector3(0,1,0)* 4.0f * mouseMoveX);
	}
}
