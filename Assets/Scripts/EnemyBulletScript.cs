using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour {

    [SerializeField] private float _speed = 50.0f;
    private float _damage = 1.0f;

    public float Damage
    {
        get { return _damage;}
        set { _damage = value; }
    }

    void Start()
    {
        Invoke("Kill", 2.0f);
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
        if(other.tag == "Player" || other.tag == "Plant1" || other.tag == "Plant2" || other.tag == "Plant3")
        {
            HealthScript health = other.GetComponent<HealthScript>();

            if (health == null) return;

            //replace later with player damage
            health.Damage(_damage);
            Destroy(gameObject);
        }
        else if(other.tag == "Factory")
        {
            Destroy(gameObject);
        }

       
    }
}
