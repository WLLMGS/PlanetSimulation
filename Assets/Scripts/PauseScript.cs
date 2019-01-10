using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;

    private bool _isPaused = false;
   

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isPaused)
            {
               UnPause();
            }
            else
            {
               Pause();
            }
        }
    }

    private void Pause()
    {
        //game is paused
        _isPaused = true;

        //enable paused menu
        _pauseMenu.SetActive(true);

        //set time scale to zero
        Time.timeScale = 0.0f;

        //disable camera movement (time independent)
        HorizontalRotation.CanRotate = false;

        //unlock cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    private void UnPause()
    {
        //game is unpaused
        _isPaused = false;

        //disable pause menu
        _pauseMenu.SetActive(false);

        //set time scale to one
        Time.timeScale = 1;

        //enable camera movement
        HorizontalRotation.CanRotate = true;

        //lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void ResumeButton()
    {
        UnPause();
    }

    public void RestartButton()
    {
        //delete all enemies
        EnemyManager.Instance.RemoveAllEnemies();
        //delete all plants
        PlantManager.Instance.RemoveAllPlants();

        //go to victory scene
        Destroy(GameObject.Find("Canvas"));

        //destroy the game managers
        Destroy(GameObject.Find("Managers"));

        //destroy the planet
        Destroy(GameObject.Find("plant_1"));

        //destroy player
        Destroy(GameObject.Find("Player"));

        //destroy factory
        Destroy(GameObject.FindGameObjectWithTag("Factory"));

        //make cursor visible
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        UnPause();

        SceneManager.LoadScene(1);

    }

    public void QuitButton()
    {
        Application.Quit();
    }

}
