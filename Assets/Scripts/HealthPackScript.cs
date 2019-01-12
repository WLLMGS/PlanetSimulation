using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPackScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider
        other)
    {
        //when collided with player
        if (other.tag == "Player")
        {
            //get health comp
            var health = other.GetComponent<HealthScript>();
            if (health == null) return;

            //full heal the player
            health.CurrentHealth = health.MaxHealth;

            //play pick up sound
            var source = other.GetComponents<AudioSource>()[1];

            if (!source.isPlaying)
            {
                source.Play();
            }
            else
            {
                source.Stop();
                source.Play();
            }

            //destroy the health pack
            Destroy(gameObject);
        }
    }
}
