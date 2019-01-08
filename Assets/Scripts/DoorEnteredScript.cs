using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorEnteredScript : MonoBehaviour {
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            UIScript.Instance.SetObjective("Destroy The Factory");
           GameplayManager.Instance.NotifyAdvanceGame();
        }
    }
}
