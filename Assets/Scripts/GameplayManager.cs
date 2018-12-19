using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayManager : MonoBehaviour
{

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
    private int _amountOfSeeds = 0;
    private UIScript _UIManager = null;
    [SerializeField] private bool _IsTutorialDone = false;
    [SerializeField] private GameObject _storePrefab;
    [SerializeField] private GameObject _tooltip;
    [SerializeField] private GameObject _shop;


    public int Seeds
    {
        get
        {
            return _amountOfSeeds;
        }
    }

    public bool IsTutorialDone
    {
        get
        {
            return _IsTutorialDone;
        }
        set
        {
            _IsTutorialDone = value;
        }
    }

    void Start()
    {
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

    public void NotifyPlayerDeath()
    {
        Debug.Log("GAME OVER");
        //go to game over scene
        SceneManager.LoadScene(1);
    }

    public void NotifyFactoryDestroyed(Transform t)
    {
        //spawn store at the location
        var store = Instantiate(_storePrefab, t.position, t.rotation);
        store.GetComponent<StoreAccessScript>()._tooltip = _tooltip;
        store.GetComponent<StoreAccessScript>()._shop = _shop;
        Destroy(t.gameObject);
    }
}
