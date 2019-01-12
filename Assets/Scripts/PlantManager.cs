using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlantManager : MonoBehaviour
{

    private static PlantManager _instance = null;
    private UIScript _UIManager;
    private int _friendPlant1Cap = 5;
    private int _friendPlant2Cap = 5;
    private int _friendPlant3Cap = 5;


    private List<GameObject> _friendlyPlants_1 = new List<GameObject>();
    private List<GameObject> _friendlyPlants_2 = new List<GameObject>();
    private List<GameObject> _friendlyPlants_3 = new List<GameObject>();


    public static PlantManager Instance
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

    private void Start()
    {
        _UIManager = UIScript.Instance;

        _friendPlant1Cap = PlayerStats.FriendlyPlant1Cap;
        _friendPlant2Cap = PlayerStats.FriendlyPlant2Cap;
        _friendPlant3Cap = PlayerStats.FriendlyPlant3Cap;

        _UIManager.SetMaxAmountPlant1(_friendPlant1Cap);
        _UIManager.SetMaxAmountPlant2(_friendPlant2Cap);
        _UIManager.SetMaxAmountPlant3(_friendPlant3Cap);
    }

    private void Update()
    {
        //remove objs in list where item is null
        _friendlyPlants_1 = _friendlyPlants_1.Where(item => item != null).ToList();
        _friendlyPlants_2 = _friendlyPlants_2.Where(item => item != null).ToList();
        _friendlyPlants_3 = _friendlyPlants_3.Where(item => item != null).ToList();
    }

    //register first plant and update UI
    public void RegisterPlant1(GameObject plant)
    {
        if (_friendlyPlants_1.Count >= _friendPlant1Cap)
        {
             RemoveOldestAndShift(_friendlyPlants_1);
        }

        _friendlyPlants_1.Add(plant);

        _UIManager.SetCurrentAmountPlant1(_friendlyPlants_1.Count);

    }
    //register second plant and update UI
    public void RegisterPlant2(GameObject plant)
    {
        if (_friendlyPlants_2.Count >= _friendPlant2Cap)
        {
            RemoveOldestAndShift(_friendlyPlants_2);
        }

        _friendlyPlants_2.Add(plant);

        _UIManager.SetCurrentAmountPlant2(_friendlyPlants_2.Count);
    }
    //register third plant and update UI
    public void RegisterPlant3(GameObject plant)
    {
        if (_friendlyPlants_3.Count >= _friendPlant3Cap)
        {
            RemoveOldestAndShift(_friendlyPlants_3);
        }
        _friendlyPlants_3.Add(plant);

        _UIManager.SetCurrentAmountPlant3(_friendlyPlants_3.Count);
    }
    //unregister first plant and update UI
    public void UnregisterPlant1(GameObject plant)
    {
        _friendlyPlants_1.Remove(plant);
        _UIManager.SetCurrentAmountPlant1(_friendlyPlants_1.Count);
    }
    //unregister second plant and update UI
    public void UnregisterPlant2(GameObject plant)
    {
        _friendlyPlants_2.Remove(plant);
        _UIManager.SetCurrentAmountPlant2(_friendlyPlants_2.Count);
    }
    //unregister third plant and update UI
    public void UnregisterPlant3(GameObject plant)
    {
        _friendlyPlants_3.Remove(plant);
        _UIManager.SetCurrentAmountPlant3(_friendlyPlants_3.Count);
    }

    //remove the oldest object in the list
    private void RemoveOldestAndShift(List<GameObject> list)
    {
        Destroy(list[0]);
        list.RemoveAt(0);
    }
    //remove all plants and update the UI
    public void RemoveAllPlants()
    {
        foreach (var plant in _friendlyPlants_1)
        {
            Destroy(plant);
        }

        foreach (var plant in _friendlyPlants_2)
        {
            Destroy(plant);
        }

        foreach (var plant in _friendlyPlants_3)
        {
            Destroy(plant);
        }

        _friendlyPlants_1.Clear();
        _friendlyPlants_2.Clear();
        _friendlyPlants_3.Clear();

        _UIManager.SetCurrentAmountPlant1(_friendlyPlants_1.Count);
        _UIManager.SetCurrentAmountPlant2(_friendlyPlants_2.Count);
        _UIManager.SetCurrentAmountPlant3(_friendlyPlants_3.Count);

    }
    //get the closest plant based on 
    public Transform GetClosestPlant(Transform other)
    {
        Transform result = null;
        float shortestDistance = 1000.0f;

        foreach (GameObject g in _friendlyPlants_1)
        {
            float distance = Vector3.Distance(g.transform.position, other.position);

            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                result = g.transform;
            }
        }

        foreach (GameObject g in _friendlyPlants_2)
        {
            float distance = Vector3.Distance(g.transform.position, other.position);

            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                result = g.transform;
            }
        }

        foreach (GameObject g in _friendlyPlants_3)
        {
            float distance = Vector3.Distance(g.transform.position, other.position);

            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                result = g.transform;
            }
        }


        return result;
    }
    //update the max cap for plant 1
    public void UpdateCap1()
    {
        _friendPlant1Cap = PlayerStats.FriendlyPlant1Cap;
    }
    //update the max cap for plant 2
    public void UpdateCap2()
    {
        _friendPlant2Cap = PlayerStats.FriendlyPlant2Cap;
    }
    //update the max cap for plant 3
    public void UpdateCap3()
    {
        _friendPlant3Cap = PlayerStats.FriendlyPlant3Cap;
    }

}
