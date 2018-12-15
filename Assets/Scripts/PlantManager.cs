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
    }

    public void RegisterPlant1(GameObject plant)
    {
        if (_friendlyPlants_1.Count >= _friendPlant1Cap)
        {
             RemoveOldestAndShift(_friendlyPlants_1);
        }

        _friendlyPlants_1.Add(plant);

        _UIManager.SetCurrentAmountPlant1(_friendlyPlants_1.Count);

    }

    public void RegisterPlant2(GameObject plant)
    {
        if (_friendlyPlants_2.Count >= _friendPlant2Cap)
        {
            RemoveOldestAndShift(_friendlyPlants_2);
        }

        _friendlyPlants_2.Add(plant);

        _UIManager.SetCurrentAmountPlant1(_friendlyPlants_2.Count);
    }

    public void RegisterPlant3(GameObject plant)
    {
        if (_friendlyPlants_3.Count >= _friendPlant3Cap)
        {
            RemoveOldestAndShift(_friendlyPlants_3);
        }
        _friendlyPlants_3.Add(plant);

        _UIManager.SetCurrentAmountPlant1(_friendlyPlants_3.Count);
    }

    public void UnregisterPlant1(GameObject plant)
    {
        _friendlyPlants_1.Remove(plant);
        _UIManager.SetCurrentAmountPlant1(_friendlyPlants_1.Count);
    }

    public void UnregisterPlant2(GameObject plant)
    {
        _friendlyPlants_2.Remove(plant);
        _UIManager.SetCurrentAmountPlant1(_friendlyPlants_2.Count);
    }

    public void UnregisterPlant3(GameObject plant)
    {
        _friendlyPlants_3.Remove(plant);
        _UIManager.SetCurrentAmountPlant1(_friendlyPlants_3.Count);
    }


    private void RemoveOldestAndShift(List<GameObject> list)
    {
        Destroy(list[0]);
        list.RemoveAt(0);
    }

    
    public Transform GetClosestPlant(Transform other)
    {
        Transform result = null;
        float shortestDistance = 1000.0f;

        _friendlyPlants_1 = _friendlyPlants_1.Where(item => item != null).ToList();

        foreach (GameObject g in _friendlyPlants_1)
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

}
