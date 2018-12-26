using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorEnteredScript : MonoBehaviour {
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
           GameplayManager.Instance.NotifyAdvanceGame();
        }
    }
}
