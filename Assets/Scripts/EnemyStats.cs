using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
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
