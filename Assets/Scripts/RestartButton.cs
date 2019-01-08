using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class RestartButton : MonoBehaviour {

    private bool _IsHovering = false;
    private Text _text;


    private void Start()
    {
        _text = GetComponentInChildren<Text>();
    }

    private void Update()
    {
        if (_IsHovering
            && Input.GetMouseButtonDown(0))
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
           

            //reset gamestage
            gm.GameStage = 1;

            //reset spawn timer
            GameplayManager.Instance.ResetSpawnerTimer();

            //load new scene
            SceneManager.LoadScene(3);
        }
    }

    private void OnMouseEnter()
    {
        _IsHovering = true;
        _text.color = Color.gray;
    }

    private void OnMouseExit()
    {
        _IsHovering = false;
        _text.color = Color.white;
    }
}
