using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public static float EnemyHealth = 7.0f;
    public static float EnemyDamage = 1.0f;
    public static float FactoryHealth = 250.0f;
   

    private Transform _player;
    private float _approachRange = 20.0f;
    
    public Transform Player
    {
        get { return _player; }
        set { _player = value; }
    }

    public float ApproachRange
    {
        get { return _approachRange; }
        set { _approachRange = value; }
    }
}
