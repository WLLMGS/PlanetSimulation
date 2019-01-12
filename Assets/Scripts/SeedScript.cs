using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedScript : MonoBehaviour {

    private int _value = 1;

    public int Value
    {
        get
        {
            return _value;
        }
        set
        {
            _value = value;
        }
    }

    private void Update()
    {
        //rotate the obj 360 degrees per second
        transform.Rotate(new Vector3(0, 360.0f * Time.deltaTime, 0));
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if(other.tag == "Player")
        {
            //when the player enters the trigger add the seeds to the total amount in the gameplay manager
            GameplayManager.Instance.AddSeeds(_value);

            //play the pickup sound clip
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
            //destroy seed object
            Destroy(gameObject);
        }
    }

}
