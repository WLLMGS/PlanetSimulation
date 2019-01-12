using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private float _speed = 50.0f;
    private GameplayManager _gamemanager;

    void Start()
    {
        _gamemanager = GameplayManager.Instance;
        Invoke("Kill", 0.5f); //destroy bullet after half a second
    }

    void Kill()
    {
        Destroy(gameObject);
    }

    void Update()
    {
        //move the bullet forward
        transform.position += transform.right * _speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            //destroy bullet
            Destroy(gameObject);

            //get health & enemy beh scritp
            HealthScript health = other.GetComponent<HealthScript>();
            EnemyBehavior beh = other.GetComponent<EnemyBehavior>();

            //if no health return
            if (health == null ) return;

            //replace later with player damage
            health.Damage(PlayerStats.PlayerDamage);

            //if no beh return
            if (beh == null) return;

            beh.IsHitByPlayer = true;

        }
        else if (other.tag == "Factory")
        {
            //only hurt factory with bullets if the tutorial is done
            if (_gamemanager.IsTutorialDone)
            {
                HealthScript health = other.GetComponent<HealthScript>();
                if (health == null) return;

                health.Damage(PlayerStats.PlayerDamage);
            }
        }
        else if(other.tag == "TutorialEnemy")
        {
            //destroy bullet
            Destroy(gameObject);

            HealthScript health = other.GetComponent<HealthScript>();

            //if no health return
            if (health == null) return;

            //replace later with player damage
            health.Damage(PlayerStats.PlayerDamage);
        }

    }
}
