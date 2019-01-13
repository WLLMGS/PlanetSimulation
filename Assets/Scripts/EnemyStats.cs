using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyStats : MonoBehaviour
{
    //static factory health
    public static float FactoryHealth = 300.0f;
    //basic stats
    [SerializeField] public float EnemyHealth = 7.0f;
    [SerializeField] public float EnemyDamage = 1.0f;
    [SerializeField] public float EnemyDamageStageBonus = 0.25f;

    private Transform _player;
    private float _approachRange = 20.0f;

    //stores player location
    public Transform Player
    {
        get { return _player; }
        set { _player = value; }
    }
    //stores approach range for ai
    public float ApproachRange
    {
        get { return _approachRange; }
        set { _approachRange = value; }
    }
}
