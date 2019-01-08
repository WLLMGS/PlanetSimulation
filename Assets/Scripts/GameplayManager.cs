﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

public class GameplayManager : MonoBehaviour
{

    //============== Instance ==============
    private static GameplayManager _instance = null;
    
    public static GameplayManager Instance
    {
        get
        {
            return _instance;
        }
        
    }

    private void Awake()
    {

        DontDestroyOnLoad(gameObject);
        
        if (_instance == null) _instance = this;


    }

    //============== Camera ==============
    [SerializeField] private Camera _camera;
    public Camera Camera
    {
        get
        {
            return _camera;
        }
    }

    //============== Other ==============
    private int _amountOfSeeds = 0;
    private UIScript _UIManager = null;
    [FormerlySerializedAs("_IsTutorialDone")] [SerializeField] private bool _isTutorialDone = false;
    [FormerlySerializedAs("_FactoryPrefab")] [SerializeField] private GameObject _factoryPrefab;
    [SerializeField] private Transform _FactorySpawnPoint;
    

    [SerializeField] private GameObject _storePrefab;
    [SerializeField] private GameObject _shop;
    [SerializeField] private GameObject _Door;
    
    private int _GameStage = 1;


    private GameObject _currentFactory;

    public GameObject CurrentFactory
    {
        get { return _currentFactory; }
    }

    public int Seeds
    {
        get
        {
            return _amountOfSeeds;
        }
    }

    public void ClearSeeds()
    {
        _amountOfSeeds = 0;
    }

    public bool IsTutorialDone
    {
        get
        {
            return _isTutorialDone;
        }
        set
        {
            _isTutorialDone = value;
        }
    }

    public int GameStage
    {
        get { return _GameStage; }
        set { _GameStage = 1; }
    }

    void Start()
    {
        _UIManager = UIScript.Instance;

        SpawnFactory();
        _currentFactory.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SceneManager.LoadScene(3);
        }
    }

    void SpawnFactory()
    {
        var inst = Instantiate(_factoryPrefab, _FactorySpawnPoint.position, _FactorySpawnPoint.rotation);
        _currentFactory = inst;
        DontDestroyOnLoad(inst);
    }

    public void AddSeeds(int amount)
    {
        if (amount > 0) _amountOfSeeds += amount;
    }
    public bool UseSeeds(int amount)
    {
        if (_amountOfSeeds >= amount)
        {
            _UIManager.ActivateDeductionUI(amount);
            _amountOfSeeds -= amount;
            return true;
        }
        return false;
    }

    public void NotifyPlayerDeath()
    {
        Debug.Log("GAME OVER");
        //go to game over scene
        SceneManager.LoadScene(2);
    }

    public void NotifyFactoryDestroyed(Transform t)
    {
        Debug.Log("FACTORY DESTROYED");
       

        //update objective
        _UIManager.SetObjective("Go Through The Door To Continue");

        //remove all current plants
        PlantManager.Instance.RemoveAllPlants();

        //remove all enemies
        EnemyManager.Instance.RemoveAllEnemies();


        if (_GameStage < 3)
        {
            //spawn store at the location
            Vector3 storePos = t.position + t.right * -10.0f;

            var store = Instantiate(_storePrefab, storePos, t.rotation);
            store.GetComponent<StoreAccessScript>()._shop = _shop;

            //spawn door to go to next level
            Vector3 doorPos = t.position + t.right * 10.0f;
            Instantiate(_Door, doorPos, t.rotation);
        }
        


        Destroy(t.gameObject);
    }

    public void NotifyAdvanceGame()
    {
        if (_GameStage == 1)
        {
            EnemySpawner._spawnAmount += 3;

            ++_GameStage;
            SceneManager.LoadScene(3);
            SpawnFactory();
        }
        else if (_GameStage == 2)
        {
            ++_GameStage;
            EnemySpawner._spawnAmount += 3;
            SceneManager.LoadScene(3);
            SpawnFactory();

        }
        
    }

    public void ActivateFactory()
    {
        _currentFactory.SetActive(true);
    }

    public void ResetSpawnerTimer()
    {
        _currentFactory.GetComponentInChildren<EnemySpawner>().Timer = 0.0f;
    }
}
