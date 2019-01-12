using UnityEngine;
using System.Collections;

public class CameraFacingBillboard : MonoBehaviour
{
    public Camera m_Camera;

    private void Start()
    {
        //get the camera from the game manager
        m_Camera = GameplayManager.Instance.Camera;
    }

    //Orient the camera after all movement is completed this frame to avoid jittering
    void LateUpdate()
    {
        //in the late update -> check if there is a camera
        //rotate the gameobject towards the camera
        if(m_Camera)
        {
            transform.LookAt(transform.position + m_Camera.transform.rotation * Vector3.forward,
                m_Camera.transform.rotation * Vector3.up);

        }
    }
}