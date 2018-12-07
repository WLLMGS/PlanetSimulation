﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    [SerializeField] private GameObject _enemyPrefab = null;
    [SerializeField] private Transform _player;

    private float _spawnrate = 10.0f;
    private int _spawnAmount = 5;

    private int _spawnAmountCurrentWave = 0;

    private float _timer = 0;

	void Start () {
        _player = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        _timer -= Time.deltaTime;

        if(_timer <= 0.0f)
        {
            _timer = _spawnrate;
            SpawnWave();
        }

    }

    private void SpawnWave()
    {
        //set the amount of enemies that need to be spawned this wave
        _spawnAmountCurrentWave = _spawnAmount;
        //start coroutine that spawns enemies one by one
        StartCoroutine( SpawnEnemyCoroutine());
    }


    private void SpawnEnemy()
    {
        //calculate random pos around factory
        Vector3 pos = transform.position;

        float horz = Random.Range(-3.0f, 3.0f);
        float vert = Random.Range(-3.0f, 3.0f);

        pos += transform.forward * vert;
        pos += transform.right * horz;
        pos += transform.up * -0.5f;

        //spawn enemy at that position and set target to player
        GameObject en = Instantiate(_enemyPrefab, pos, Quaternion.identity);
        en.GetComponent<EnemyBehavior>().Target = _player;
    }

   IEnumerator SpawnEnemyCoroutine()
    {
        yield return new WaitForSeconds(0.2f);

        //spawn enemy on random location around factory
        SpawnEnemy();

        //decrement amount of enemies to spawn
        --_spawnAmountCurrentWave;
        //check if there are any more enemies that need to be spawned in this wave
        if (_spawnAmountCurrentWave > 0)StartCoroutine( SpawnEnemyCoroutine());

    }




}
