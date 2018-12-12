using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private float _speed = 50.0f;

    void Start()
    {
        Invoke("Kill", 0.5f);
    }

    void Kill()
    {
        Destroy(gameObject);
    }

    void Update()
    {
        transform.position += transform.right * _speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            HealthScript health = other.GetComponent<HealthScript>();
            EnemyBehavior beh = other.GetComponent<EnemyBehavior>();

            if (health == null || beh == null) return;

            //replace later with player damage
            health.Damage(1.0f);
            beh.IsHitByPlayer = true;


            Destroy(gameObject);
        }
        else if(other.tag == "Factory")
        {
            HealthScript health = other.GetComponent<HealthScript>();
            if (health == null) return;

            health.Damage(1.0f);
        }

    }
}
