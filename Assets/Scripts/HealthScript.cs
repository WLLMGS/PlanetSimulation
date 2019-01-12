using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthScript : MonoBehaviour
{

    [SerializeField] private float _maxHealth;
    [SerializeField] private Slider _healthbar;
    [SerializeField] private bool _doFlicker = true;

    private List<Material> _mats = new List<Material>();
    private List<Color> _originalColor = new List<Color>();

    private float _currentHeath;
    private bool _DidOnDeathEvent = false;

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

        Init();

        UpdateUI(); //update UI

        //get the attached material
        Renderer[] renderers = GetComponentsInChildren<Renderer>();

        //if flicker on damage is turned on and there is at least one renderer
        if (_doFlicker
        && renderers.Length > 0)
        {
            foreach (Renderer r in renderers)
            {
                //add material to list
                _mats.Add(r.material);
                if (r.material.color != null)
                {
                    //add original color to list
                    _originalColor.Add(r.material.color);
                }
            }
        }


    }

    private void Init()
    {
        //depending on the tag set the current health and max health
        if (gameObject.tag == "Player")
        {
            _maxHealth = PlayerStats.PlayerHealth;
            _currentHeath = _maxHealth;
        }
        else if (gameObject.tag == "Plant1"
                 || gameObject.tag == "Plant2"
                 || gameObject.tag == "Plant3")
        {
            _maxHealth = PlayerStats.PlantHealth;
            _currentHeath = _maxHealth;
        }
        else if (gameObject.tag == "Enemy")
        {
            _maxHealth = GetComponent<EnemyStats>().EnemyHealth;
            _currentHeath = _maxHealth;
        }
        else if (gameObject.tag == "Factory")
        {
            _maxHealth = EnemyStats.FactoryHealth * GameplayManager.Instance.GameStage;
            _currentHeath = _maxHealth;
        }
    }

    //update current health bar
    void UpdateUI()
    {
        if (_healthbar) _healthbar.value = _currentHeath / _maxHealth;
    }

    //check if obj still alive
    void CheckIfAlive()
    {
        //replace later with custom events depending on factory, enemy, player
        if (_currentHeath <= 0
            && !_DidOnDeathEvent)
        {
            //if obj is dead do a event on death
            _DidOnDeathEvent = true;
          
            DoOnDeathEvent();
           
        }
    }

    //depending on the tag do something when obj dies
    void DoOnDeathEvent()
    {
        switch (gameObject.tag)
        {
            case "Enemy":
                var enemyBeh = gameObject.GetComponent<EnemyBehavior>();
                var lootComp = gameObject.GetComponent<LootDropScript>();
                if (enemyBeh != null)
                {
                    if (!enemyBeh.IsDead)
                    {
                        if (lootComp == null) break;
                        lootComp.DropLoot();
                    }
                    enemyBeh.IsDead = true;
                    return;
                }

                var meleebehavior = gameObject.GetComponent<MeleeEnemyBehavior>();

                if (meleebehavior != null)
                {
                    if (!meleebehavior.IsDead)
                    {
                        if (lootComp == null) break;
                        lootComp.DropLoot();
                        meleebehavior.IsDead = true;
                    }
                    return;
                }

                break;
            case "TutorialEnemy":
                gameObject.GetComponentInChildren<Animator>().SetTrigger("Die");
                gameObject.GetComponent<LootDropScript>().DropLoot();
                StartCoroutine(DestroyObj());
                break;
            case "Player":
                //game over
                GameplayManager.Instance.NotifyPlayerDeath();
                break;
            case "Plant1":
                PlantManager.Instance.UnregisterPlant1(gameObject);
                Destroy(gameObject);
                break;
            case "Plant2":
                PlantManager.Instance.UnregisterPlant2(gameObject);
                Destroy(gameObject);
                break;
            case "Plant3":
                PlantManager.Instance.UnregisterPlant3(gameObject);
                Destroy(gameObject);
                break;
            case "Factory":
                var loot = GetComponent<LootDropScript>();
                if (loot)
                {
                    loot.DropLoot();
                }
                GameplayManager.Instance.NotifyFactoryDestroyed(gameObject.transform);
                break;
            default:
                Destroy(gameObject);
                break;
        }
    }

    //flicker material when obj gets damaged
    void DamageFlickerAnimation()
    {
        foreach (Material m in _mats)
        {
            m.color *= new Color(1, 0, 0, 1);
        }

        StartCoroutine(ChangeBackToNormalColor());
    }
    //change back to original color after 0.1s
    IEnumerator ChangeBackToNormalColor()
    {
        yield return new WaitForSeconds(0.1f);

        for (int i = 0; i < _mats.Count; ++i)
        {
            _mats[i].color = _originalColor[i];
        }
    }
    //destroy current game object after 1s
    IEnumerator DestroyObj()
    {
        yield return new WaitForSeconds(1.0f);
        TutorialManager.Instance.IncrementTutorialStage();
        Destroy(gameObject);
    }
    //damage obj
    public void Damage(float amount)
    {
        //redeuce current health with amount
        _currentHeath -= amount;
        //clamp health between zero and max health
        Mathf.Clamp(_currentHeath, 0, _maxHealth);
        //check if still alive
        CheckIfAlive();
        //update health bar ui
        UpdateUI();
        //do flicker animation 
        if (_doFlicker) DamageFlickerAnimation();

        if (tag == "Player"
            || tag == "Enemy"
            || tag == "Factory"
            || tag == "TutorialEnemy")
        {
            //play damage sound
            AudioSource source = GetComponent<AudioSource>();
            if (!source.isPlaying)
            {
                source.Play();
            }
            else
            {
                source.Stop();
                source.Play();
            }
        }
    }
}
