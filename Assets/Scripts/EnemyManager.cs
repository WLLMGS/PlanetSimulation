using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyManager : MonoBehaviour
{

    private static EnemyManager _instance = null;
    private static int ENEMY_CAP = 10;

    private List<GameObject> _enemies = new List<GameObject>();

    public static EnemyManager Instance
    {
        get
        {
            return _instance;
        }
    }

    public int CurrentAmountOfEnemies
    {
        get
        {
            return _enemies.Count;
        }
    }

    private void Awake()
    {
        if (_instance == null) _instance = this;
    }

    public void RegisterEnemy(GameObject enemy)
    {
        if (!_enemies.Contains(enemy)) _enemies.Add(enemy);
    }

    private void Update()
    {
        _enemies = _enemies.Where(item => item != null).ToList();
    }

    public GameObject GetClosestEnemy(Transform other)
    {
        GameObject closest = null;
        float shortestDistance = 1000.0f;

        foreach (var en in _enemies)
        {
            float distance = Vector3.Distance(other.position, en.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closest = en;
            }
        }
        
        return closest;
    }

    public void RemoveAllEnemies()
    {
        foreach (var enemy in _enemies)
        {
            Destroy(enemy);
        }
    }

    public bool HasCapBeenReached()
    {
        _enemies = _enemies.Where(item => item != null).ToList();

        return (_enemies.Count >= ENEMY_CAP);
    }

}
