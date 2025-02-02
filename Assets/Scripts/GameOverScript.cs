﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            var gm = GameplayManager.Instance;

            //reset factory health
            var health = gm.CurrentFactory.GetComponent<HealthScript>();
            health.CurrentHealth = health.MaxHealth;

            //clear seeds
            gm.ClearSeeds();

            //clear enemies
            EnemyManager.Instance.RemoveAllEnemies();
            //clear plants
            PlantManager.Instance.RemoveAllPlants();
            //update UI

            //reset gamestage
            gm.GameStage = 1;

            //load new scene
            SceneManager.LoadScene(3);
        }
    }
}
