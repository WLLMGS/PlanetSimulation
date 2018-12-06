using System.Collections;
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
        _spawnAmountCurrentWave = _spawnAmount;
        StartCoroutine( SpawnEnemy());
    }

   IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(0.2f);
        GameObject en = Instantiate(_enemyPrefab, transform.position, Quaternion.identity);
        en.GetComponent<EnemyBehavior>().Target = _player;

        --_spawnAmountCurrentWave;

        if (_spawnAmountCurrentWave > 0)StartCoroutine( SpawnEnemy());

    }


}
