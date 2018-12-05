using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour {

    [SerializeField] private float _maxHealth;
    [SerializeField] private Slider _healthbar;

    private float _currentHeath;


    public float MaxHealth
    {
        get
        {
            return _maxHealth;
        }
        set
        {
            _maxHealth = value;
        }
    }

    public float CurrentHealth
    {
        get
        {
            return _currentHeath;
        }
        set
        {
            _currentHeath = value; //set current health to the value;
            Mathf.Clamp(_currentHeath, 0, _maxHealth); //clamp the health between 0 and max health
            CheckIfAlive(); //check if the entity is still alive
            UpdateUI();//update UI if healthbar is set

        }
    }

    private void Start()
    {
        _currentHeath = _maxHealth;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (_healthbar) _healthbar.value = _currentHeath / _maxHealth;
    }

    void CheckIfAlive()
    {
        //replace later with custom events depending on factory, enemy, player
        if (_currentHeath <= 0) Destroy(gameObject);
    }
}
