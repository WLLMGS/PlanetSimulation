using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayManager : MonoBehaviour {

    //============== Instance ==============
    private static GameplayManager _instance = null;

    public static GameplayManager Instance
    {
        get
        {
            return _instance;
        }
    }
    private void Awake()
    {
        if (_instance == null) _instance = this;

        
    }

    //============== Camera ==============
    [SerializeField] private Camera _camera;

    public Camera Camera
    {
        get
        {
            return _camera;
        }
    }


    //============== Other ==============
    [SerializeField] private GameObject _canvas;
   

    void Start () {
        
        //spawn canvas for UI
        Instantiate(_canvas, transform.position, Quaternion.identity);

        
    }
	
}
