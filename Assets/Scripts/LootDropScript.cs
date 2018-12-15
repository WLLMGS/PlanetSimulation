using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootDropScript : MonoBehaviour {

    [SerializeField] private GameObject _lootPrefab;

    [SerializeField] private int _valueMin = 1;
    [SerializeField] private int _valueMax = 5;
    private bool _isLootDropped = false;

    public void DropLoot()
    {
        if(!_isLootDropped)
        {
            Vector3 pos = transform.position + transform.up / 3.0f;

            var loot = Instantiate(_lootPrefab, pos, transform.rotation);
            var seedComp = loot.GetComponent<SeedScript>();
            if (seedComp == null) return;
            seedComp.Value = Random.Range(_valueMin, _valueMax);

            _isLootDropped = true;
        }
        
    }
}
