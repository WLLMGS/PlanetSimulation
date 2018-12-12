using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthScript : MonoBehaviour {

    [SerializeField] private float _maxHealth;
    [SerializeField] private Slider _healthbar;
    private List<Material> _mats = new List<Material>();
   
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

        //get the attached material
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        
        foreach(Renderer r in renderers)
        {
            _mats.Add(r.material);
        }

    }

    void UpdateUI()
    {
        if (_healthbar) _healthbar.value = _currentHeath / _maxHealth;
    }

    void CheckIfAlive()
    {
        //replace later with custom events depending on factory, enemy, player
        if (_currentHeath <= 0)
        {
            DoOnDeathEvent();
            //Destroy(gameObject);
        } 
    }

    void DoOnDeathEvent()
    {
        switch(gameObject.tag)
        {
            case "Enemy":
                var enemyBeh = gameObject.GetComponent<EnemyBehavior>();
                if (enemyBeh == null) return;

                if (!enemyBeh.IsDead)
                {
                    var lootComp = gameObject.GetComponent<LootDropScript>();
                    if (lootComp == null) break;
                    lootComp.DropLoot();
                }

                enemyBeh.IsDead = true;
                break;
            default:
                Destroy(gameObject);
                break;
        }
    }


    void DamageFlickerAnimation()
    {

        foreach (Material m in _mats)
        {

            m.color *= new Color(1, 0, 0, 1);
        }

        StartCoroutine(ChangeBackToNormalColor());
    }
    IEnumerator ChangeBackToNormalColor()
    {
        yield return new WaitForSeconds(0.1f);

        foreach (Material m in _mats)
        {
            m.color = new Color(1, 1, 1, 1);
        }
    }

    public void Damage(float amount)
    {
        _currentHeath -= amount;
        Mathf.Clamp(_currentHeath, 0, _maxHealth);
        CheckIfAlive();
        UpdateUI();
        DamageFlickerAnimation();
    }
}
