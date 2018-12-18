using System;
using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public struct SpawnableEnemy
{
    public GameObject _enemy;
    public int _rangeMin;
    public int _rangeMax;
}

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<SpawnableEnemy> _enemyPrefabs = new List<SpawnableEnemy>();
    private int _maxWeightRange;


    [SerializeField] private GameObject _enemyPrefab = null;
    [SerializeField] private Transform _player;

    private EnemyManager _enemyManager;
    private GameplayManager _gamemanager;

    private float _spawnrate = 10.0f;
    private int _spawnAmount = 5;

    private int _spawnAmountCurrentWave = 0;

    private float _timer = 0;
    private bool _isPlayerInRange = false;

    void Start()
    {
        _player = GameObject.Find("Player").transform;
        _enemyManager = EnemyManager.Instance;
        _gamemanager = GameplayManager.Instance;

        _maxWeightRange = _enemyPrefabs[_enemyPrefabs.Count - 1]._rangeMax;

        Debug.Log(_maxWeightRange);
    }

    private void Update()
    {
        if(_gamemanager.IsTutorialDone) HandleSpawning();
    }

    private void HandleSpawning()
    {
        _timer -= Time.deltaTime;

        if (_timer <= 0.0f)
        {
            if (_isPlayerInRange
                && !_enemyManager.HasCapBeenReached())
            {
                _timer = _spawnrate;
                SpawnWave();
            }
        }
    }

    private void SpawnWave()
    {
        //set the amount of enemies that need to be spawned this wave
        int amount = Random.Range(_spawnAmount - 2, _spawnAmount);
        _spawnAmountCurrentWave = amount;

        //start coroutine that spawns enemies one by one
        StartCoroutine(SpawnEnemyCoroutine());
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

        GameObject randomObj = GetRandomEnemy();

        //spawn enemy at that position and set target to player
        GameObject en = Instantiate(randomObj, pos, Quaternion.identity);
        en.GetComponent<EnemyStats>().Player = _player;

    }

    private GameObject GetRandomEnemy()
    {
        int index = Random.Range(0, _maxWeightRange);

        foreach (var en in _enemyPrefabs)
        {
            if (index >= en._rangeMin && index <= en._rangeMax)
            {
                return en._enemy;
            }
        }

        return null;
    }

    IEnumerator SpawnEnemyCoroutine()
    {
        yield return new WaitForSeconds(0.2f);

        //spawn enemy on random location around factory
        SpawnEnemy();

        //decrement amount of enemies to spawn
        --_spawnAmountCurrentWave;

        //check if there are any more enemies that need to be spawned in this wave
        //check also if enemy cap is not reached yet
        if (_spawnAmountCurrentWave > 0 && !_enemyManager.HasCapBeenReached()) StartCoroutine(SpawnEnemyCoroutine());

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") _isPlayerInRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player") _isPlayerInRange = false;
    }


}
