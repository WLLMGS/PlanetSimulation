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
        //destroy bullets after half a second
        Invoke("Kill", 2.0f);
    }

    //destroys bullet
    void Kill()
    {
        Destroy(gameObject);
    }

    void Update()
    {
        //move bullet forward
        transform.position += transform.right * _speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        //check if bullet collides w friendly entity
        //do damage to the entity
        if(other.tag == "Player" || other.tag == "Plant1" || other.tag == "Plant2" || other.tag == "Plant3")
        {
            HealthScript health = other.GetComponent<HealthScript>();

            if (health == null) return;

            //replace later with player damage
            health.Damage(_damage);
            Destroy(gameObject);
        }
        //if the bullet collides w/ the factory just destroy it
        else if(other.tag == "Factory")
        {
            Destroy(gameObject);
        }

       
    }
}
