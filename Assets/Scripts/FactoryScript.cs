using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(EnemySpawner))]
public class FactoryScript : MonoBehaviour {

    private EnemySpawner _enemySpawner = null;

    private void Start()
    {
        _enemySpawner = GetComponent<EnemySpawner>();
        _enemySpawner.enabled = true;
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
           }
    }

}
