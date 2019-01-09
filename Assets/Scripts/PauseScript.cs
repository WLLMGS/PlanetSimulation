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
    }

    public void ResumeButton()
    {
        UnPause();
    }

    public void RestartButton()
    {
        var gm = GameplayManager.Instance;

        //reset factory health
        var health = gm.CurrentFactory.GetComponent<HealthScript>();
        if(health) health.CurrentHealth = health.MaxHealth;

        //clear seeds
        gm.ClearSeeds();

        //clear enemies
        EnemyManager.Instance.RemoveAllEnemies();
        //clear plants
        PlantManager.Instance.RemoveAllPlants();

        //reset player health
        var pHealth = GameObject.Find("Player").GetComponent<HealthScript>();
        pHealth.CurrentHealth = pHealth.MaxHealth;

        //reset gamestage
        gm.GameStage = 1;

        //reset spawn timer
        GameplayManager.Instance.ResetSpawnerTimer();

        //UnPause
        UnPause();

        //disable tutorial
        gm.IsTutorialDone = true;
        UIScript.Instance.SetObjective("Destroy the factory");
        GameplayManager.Instance.ActivateFactory();

        //load new scene
        SceneManager.LoadScene(3);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

}
