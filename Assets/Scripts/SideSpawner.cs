using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideSpawner : MonoBehaviour
{

    [SerializeField] private GameObject _enemy;
    [SerializeField] private Transform _player;

    private EnemyManager _enemyManager;
    private GameplayManager _gamemanager;
    private bool _playerInRange = false;
    private bool _canSpawnEnemies = true;
    private float _spawnCooldown = 2.0f;

    private void Start()
    {
        _enemyManager = EnemyManager.Instance;
        _gamemanager = GameplayManager.Instance;
    }

    private void Update()
    {
        if(_gamemanager.IsTutorialDone) HandleSpawning();
    }

    private void HandleSpawning()
    {
        if (_playerInRange && _canSpawnEnemies)
        {
            for (int i = 0; i < 2; ++i)
            {
                //spawn enemies
                Vector3 pos = transform.position;

                float horz = Random.Range(-3.0f, 3.0f);
                float vert = Random.Range(-3.0f, 3.0f);

                pos += transform.forward * vert;
                pos += transform.right * horz;
                pos += transform.up * -0.5f;

                //spawn enemy at that position and set target to player
                //spawn only if cap has not yet been reached
                if (!_enemyManager.HasCapBeenReached())
                {
                    GameObject en = Instantiate(_enemy, pos, Quaternion.identity);
                    en.GetComponent<EnemyStats>().Player = _player;
                }
            }

            _canSpawnEnemies = false;
            //invoke cooldown
            Invoke("Cooldown", _spawnCooldown);
        }
    }

    private void Cooldown()
    {
        _canSpawnEnemies = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        //also keep spawning if there are plants in range of the side building
        if (other.tag == "Player" || other.tag == "Plant1" || other.tag == "Plant2" || other.tag == "Plant3")
        {
            _playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" || other.tag == "Plant1" || other.tag == "Plant2" || other.tag == "Plant3")
        {
            _playerInRange = false;
        }
    }
}
