﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantBuyingScript : MonoBehaviour
{

    [SerializeField] private GameObject _plant1;
    [SerializeField] private GameObject _plant2;
    [SerializeField] private GameObject _plant3;

    private GameplayManager _gamemanager;

    void Start()
    {
        _gamemanager = GameplayManager.Instance;
    }

    void Update()
    {
        //spawn plant depending on which key is pressed
        //and if there are enough seeds

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //spawn plant 1
            int cost = MeleePlantBehavior.Cost;
            if (_gamemanager.UseSeeds(cost))
            {
                Instantiate(_plant1, transform.position, transform.rotation);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {   //spawn plant 2
            int cost = ChasePlantBehavior.Cost;

            if (_gamemanager.UseSeeds(cost))
            {
                Instantiate(_plant3, transform.position, transform.rotation);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            //spawn plant 3
            int cost = PeaShooterBehavior.Cost;
            if (_gamemanager.UseSeeds(cost))
            {
                Instantiate(_plant2, transform.position, transform.rotation);
            }
            
        }

    }

}
