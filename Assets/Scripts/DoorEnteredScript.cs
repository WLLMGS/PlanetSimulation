using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorEnteredScript : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (GameplayManager.Instance.GameStage != 3)
            {
                UIScript.Instance.SetObjective("Destroy The Factory");
                GameplayManager.Instance.NotifyAdvanceGame();
            }
            else
            {
                //go to victory scene
                Destroy(GameObject.Find("Canvas"));

                //destroy the game managers
                Destroy(GameObject.Find("Managers"));

                //destroy the planet
                Destroy(GameObject.Find("plant_1"));

                //destroy player
                Destroy(GameObject.Find("Player"));


                //make cursor visible
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                SceneManager.LoadScene(4);
            }
        }
    }
}
