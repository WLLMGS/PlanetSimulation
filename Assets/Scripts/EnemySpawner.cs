using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public struct SpawnableEnemy
{
    public GameObject _enemy; //enemy prefab
    public int _rangeMin; //min weight
    public int _rangeMax; //max weight
}

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private List<SpawnableEnemy> _enemyPrefabs = new List<SpawnableEnemy>();
    [SerializeField] private List<SpawnableEnemy> _enemyPrefabsStage2 = new List<SpawnableEnemy>();
    [SerializeField] private List<SpawnableEnemy> _enemyPrefabsStage3 = new List<SpawnableEnemy>();

    private int _maxWeightRange;
    private int _maxWeightRangeStage2;
    private int _maxWeightRangeStage3;

    [SerializeField] private Transform _player;

    private EnemyManager _enemyManager;
    private GameplayManager _gamemanager;

    public static float _spawnrate = 10.0f; //in seconds
    public static int _spawnAmount = 5;

    private int _spawnAmountCurrentWave = 0;

    private float _timer = 0;
    private bool _isPlayerInRange = false;

    public float Timer
    {
        get { return _timer; }
        set { _timer = value; }
    }

    void Start()
    {
        _player = GameObject.Find("Player").transform;
        _enemyManager = EnemyManager.Instance;
        _gamemanager = GameplayManager.Instance;

        //get max spawn weight for each game stage
        _maxWeightRange = _enemyPrefabs[_enemyPrefabs.Count - 1]._rangeMax;
        _maxWeightRangeStage2 = _enemyPrefabsStage2[_enemyPrefabsStage2.Count - 1]._rangeMax;
        _maxWeightRangeStage3 = _enemyPrefabsStage3[_enemyPrefabsStage3.Count - 1]._rangeMax;
    }

    private void Update()
    {
        //only handle spawning when the tutorial is done
        if(_gamemanager.IsTutorialDone) HandleSpawning();
    }

    private void HandleSpawning()
    {
        //do cooldown
        _timer -= Time.deltaTime;

        //when cooldown is done & player is in range spawn the wave

        if (_timer <= 0.0f)
        {
            if (_isPlayerInRange
                && !_enemyManager.HasCapBeenReached())
            {
                _timer = _spawnrate; //reset timer
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
    //get random enemy to spawn based on the gamestage and the weight
    private GameObject GetRandomEnemy()
    {
        if (_gamemanager.GameStage == 1)
        {
            int index = Random.Range(0, _maxWeightRange);

            foreach (var en in _enemyPrefabs)
            {
                if (index >= en._rangeMin && index <= en._rangeMax)
                {
                    return en._enemy;
                }
            }
        }
        else if(_gamemanager.GameStage == 2)
        {
            int index = Random.Range(0, _maxWeightRangeStage2);

            foreach (var en in _enemyPrefabsStage2)
            {
                if (index >= en._rangeMin && index <= en._rangeMax)
                {
                    return en._enemy;
                }
            }
        }
        else
        {
            int index = Random.Range(0, _maxWeightRangeStage3);

            foreach (var en in _enemyPrefabsStage3)
            {
                if (index >= en._rangeMin && index <= en._rangeMax)
                {
                    return en._enemy;
                }
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
