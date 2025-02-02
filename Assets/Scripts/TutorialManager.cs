﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{

    //===============INSTANCE===============
    private static TutorialManager _instance = null;

    public static TutorialManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null) _instance = this;
    }

    //===============OTHER===============
    [SerializeField] private Text _txtObjective;
    [SerializeField] private GameObject _tutorialEnemy;
    [SerializeField] private GameObject _player;
   

    private GameplayManager _gamemanager;
    private int _tutorialStage = 0;
    private bool _incrementedAfterShooting = false;

    private void Start()
    {
        _gamemanager = GameplayManager.Instance;

        _txtObjective.text = "Objective: Left Click To Shoot";
    }
    private void Update()
    {
        /*
         1. left click to shoot
         2. kill the enemy
         3. the enemy drops seeds
         4. player needs to pick up seeds
         5. spawn a power up with the seeds
            (explain the cost and max units each plant type has)
         6. destroy the factory -> objective
         */
        switch (_tutorialStage)
        {
            case 0:
                if (Input.GetMouseButton(0)
                    && !_incrementedAfterShooting)
                {
                    _incrementedAfterShooting = true;
                    Invoke("IncrementTutorialStage", 0.5f);
                }
                break;
            case 2:
                if (GameplayManager.Instance.Seeds > 0)
                    IncrementTutorialStage();
                break;
            case 3:
                if(Input.GetKeyDown(KeyCode.E))
                {
                    IncrementTutorialStage();
                }
                break;
            case 4:
                if (GameplayManager.Instance.Seeds < 5) IncrementTutorialStage();
                else if(Input.GetKeyDown(KeyCode.Alpha1))
                {
                    IncrementTutorialStage();
                }
                break;
            case 5:
                if (Input.GetKeyDown(KeyCode.E))
                {
                    _gamemanager.IsTutorialDone = true;
                    _txtObjective.text = "Objective: Destroy The Factory";
                    GameplayManager.Instance.ActivateFactory();
                    IncrementTutorialStage();
                }
                break;

            default:
                break;
        }
    }
    //increments the current tutorial stage and sets the correct objective on the HUD
    public void IncrementTutorialStage()
    {
        ++_tutorialStage;
        //change tutorial display text here
        switch (_tutorialStage)
        {
            case 1:
                _txtObjective.text = "Objective: Shoot & Kill The Enemy";
                DoTutuorialStage1();
                break;
            case 2:
                _txtObjective.text = "Objective: Collect The Loot";
                break;
            case 3:
                _txtObjective.text = "Objective: the bar below shows all the plants you can spawn.\n " +
                    "the green number above the plant shows the cost. \n" + 
                    "<Press E to continue>";
                break;
            case 4:
                _txtObjective.text = "Spawning plants costs seeds.\n " +
                    "Your current amount of seeds are displayed in the top left corner.\n" +
                    "<Spawn a plant by pressing 1>";
                break;
            case 5:
                _txtObjective.text = "the red number shows the amount of plants you currently own and the max amount you can own. \n" +
                    "<press E to end the tutorial>";
            break;

            default:
                break;
        }
    }

    //spawn the tutorial enemy in stage 1
    private void DoTutuorialStage1()
    {
        Vector3 spawnpos = _player.transform.position;
        spawnpos += _player.GetComponentInChildren<Camera>().gameObject.transform.forward * 6.0f;

        Instantiate(_tutorialEnemy, spawnpos, Quaternion.identity);

    }
}
