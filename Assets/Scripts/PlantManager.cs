using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlantManager : MonoBehaviour
{

    private static PlantManager _instance = null;
    private List<GameObject> _friendlyPlants = new List<GameObject>();

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

    public void RegisterPlant(GameObject plant)
    {
        _friendlyPlants.Add(plant);
    }

    public Transform GetClosestPlant(Transform other)
    {
        Transform result = null;
        float shortestDistance = 1000.0f;

        _friendlyPlants = _friendlyPlants.Where(item => item != null).ToList();

        foreach (GameObject g in _friendlyPlants)
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
