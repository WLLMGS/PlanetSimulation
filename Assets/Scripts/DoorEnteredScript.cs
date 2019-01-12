using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorEnteredScript : MonoBehaviour
{

    private void OnTriggerStay(Collider other)
    {
        //check if player entered the doorway
        if (other.tag == "Player")
        {
            //if the gamestage != 3
            //reset the objective
            //notify the gameplay manager to advance the game
            if (GameplayManager.Instance.GameStage != 3)
            {
                UIScript.Instance.SetObjective("Destroy The Factory");
                GameplayManager.Instance.NotifyAdvanceGame();
            }
            else //if at final level -> go to victory scene and destroy all useless game objects
            {
                //destroy canvas
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
                //go to victory scene
                SceneManager.LoadScene(4);
            }
        }
    }
}
