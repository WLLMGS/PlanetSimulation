using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideSpawner : MonoBehaviour {

    [SerializeField] private GameObject _enemy;
    [SerializeField] private Transform _player;

    private bool _playerInRange = false;
    private bool _canSpawnEnemies = true;
    private float _spawnCooldown = 2.0f;

    private void Update()
    {
        if(_playerInRange && _canSpawnEnemies)
        {
            for(int i = 0; i < 2; ++i)
            {
                //spawn enemies
                Vector3 pos = transform.position;

                float horz = Random.Range(-3.0f, 3.0f);
                float vert = Random.Range(-3.0f, 3.0f);

                pos += transform.forward * vert;
                pos += transform.right * horz;
                pos += transform.up * -0.5f;

                //spawn enemy at that position and set target to player
                GameObject en = Instantiate(_enemy, pos, Quaternion.identity);
                en.GetComponent<EnemyBehavior>().Target = _player;
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
        if(other.tag == "Player")
        {
            _playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
       if(other.tag == "Player")
        {
            _playerInRange = false;
        }
    }
}
