using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleTestScript : MonoBehaviour
{

    [SerializeField] private GameObject _particleSystem;
    [SerializeField] private List<Transform> _spawnpositions = new List<Transform>();
	private float _spawnIntervalMin = 2.0f;
	private float _spawnIntervalMax = 5.0f;
	
    void Start()
    {
        Invoke("SpawnLightning", 2.0f);
    }

    void SpawnLightning()
    {
        int amount = Random.Range(1, 4);
        for (int i = 0; i < amount; ++i)
        {
            int rindex = Random.Range(0, _spawnpositions.Count);
            Instantiate(_particleSystem, _spawnpositions[rindex].position, _spawnpositions[rindex].rotation);
        }

		float randSpawntime = Random.Range(_spawnIntervalMin,_spawnIntervalMax);
		Invoke("SpawnLightning", randSpawntime);
    }
}
