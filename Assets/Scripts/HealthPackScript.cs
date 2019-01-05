using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPackScript : MonoBehaviour {
    private void OnTriggerEnter(Collider
        other)
    {
        if (other.tag == "Player")
        {
            var health = other.GetComponent<HealthScript>();
            if (health == null) return;

            health.CurrentHealth = health.MaxHealth;

            Destroy(gameObject);
        }
    }
}
