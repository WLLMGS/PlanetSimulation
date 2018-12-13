using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyManager : MonoBehaviour
{

    private static EnemyManager _instance = null;
    private static int ENEMY_CAP = 15;

    private List<GameObject> _enemiesManager = new List<GameObject>();

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
            return _enemiesManager.Count;
        }
    }

    private void Awake()
    {
        if (_instance == null) _instance = this;
    }

    public void RegisterEnemy(GameObject enemy)
    {
        if (!_enemiesManager.Contains(enemy)) _enemiesManager.Add(enemy);
    }

    public GameObject GetClosestEnemy(Transform other)
    {
        GameObject closest = null;
        //TODO: complete later
        return closest;
    }
    public bool HasCapBeenReached()
    {
        _enemiesManager = _enemiesManager.Where(item => item != null).ToList();

        return (_enemiesManager.Count >= ENEMY_CAP);
    }

}
