﻿using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class LootDropScript : MonoBehaviour
{

    [SerializeField] private GameObject _lootPrefab;
    [SerializeField] private GameObject _healthpackPrefab;

    [SerializeField] private int _valueMin = 1;
    [SerializeField] private int _valueMax = 5;
    private bool _isLootDropped = false;

    public void DropLoot()
    {
        //if loot is not already dropped
        //drop the loot with a random value
        //or small chance to drop health pack instead
        if (!_isLootDropped)
        {
            int index = Random.Range(0, 10);

            Vector3 pos = transform.position + transform.up / 3.0f;

            if (index == 1 
                && _healthpackPrefab != null 
                && gameObject.tag != "Factory")
            {
                //spawn healthpack
                Instantiate(_healthpackPrefab, pos, transform.rotation);
            }
            else
            {
                var loot = Instantiate(_lootPrefab, pos, transform.rotation);
                var seedComp = loot.GetComponent<SeedScript>();
                if (seedComp == null) return;

                seedComp.Value =(tag == "Factory") ? Random.Range(_valueMin, _valueMax) * GameplayManager.Instance.GameStage
                    : Random.Range(_valueMin, _valueMax);
            }


            _isLootDropped = true;
        }

    }
}
