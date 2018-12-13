using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantBuyingScript : MonoBehaviour
{

    [SerializeField] private GameObject _plant1;

    private GameplayManager _gamemanager;

    void Start()
    {
        _gamemanager = GameplayManager.Instance;
    }

    void Update()
    {
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
        {
            //spawn plant 1
            int cost = MeleePlantBehavior.Cost;
            if (_gamemanager.UseSeeds(cost))
            {
                Instantiate(_plant1, transform.position, transform.rotation);
            }
        }
    }

}
