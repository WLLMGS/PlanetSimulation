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
    private int _amountOfSeeds = 0;
    private UIScript _UIManager = null;

    public int Seeds
    {
        get
        {
            return _amountOfSeeds;
        }
    }

    void Start () {
        
        //spawn canvas for UI
        Instantiate(_canvas, transform.position, Quaternion.identity);
        _UIManager = UIScript.Instance;
    }

    public void AddSeeds(int amount)
    {
        if (amount > 0) _amountOfSeeds += amount;
    }
	public bool UseSeeds(int amount)
    {
        if (_amountOfSeeds >= amount)
        {
            _UIManager.ActivateDeductionUI(amount);
            _amountOfSeeds -= amount;
            return true;
        }
        return false;
    }
}
