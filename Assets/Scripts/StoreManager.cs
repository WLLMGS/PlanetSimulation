using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class StoreManager : MonoBehaviour
{
    //==================== GENERAL ====================
    private GameplayManager _gamemanager;
    private UIScript _UIManager;
    private PlantManager _plantManager;

    private static StoreManager _instance = null;
    
    public static StoreManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if (_instance == null) _instance = this;
    }

    private int _CostIncrement = 5;

    //==================== Player Damage Upgrade ====================
    [SerializeField] private Text _txtPlayerDamageUpgradeCost;
    private static int _PlayerDamageUpgradeCost = 5;
    private float _PlayerDamageIncrement = 0.25f;

    public void BuyPlayerDamageUpgrade()
    {
        if (_gamemanager.UseSeeds(_PlayerDamageUpgradeCost))
        {
            PlayerStats.PlayerDamage += _PlayerDamageIncrement;

            Debug.Log(PlayerStats.PlayerDamage);

            _PlayerDamageUpgradeCost += _CostIncrement;
            _txtPlayerDamageUpgradeCost.text = _PlayerDamageUpgradeCost.ToString();
        }
    }

    //==================== Plant Damage Upgrade ====================
    [SerializeField] private Text _txtPlantDamageUpgradeCost;
    private static int _PlantDamageUpgradeCost = 5;
    private float _PlantDamageIncrement = 0.25f;


    public void BuyPlantDamageUpgrade()
    {
        if (_gamemanager.UseSeeds(_PlantDamageUpgradeCost))
        {
            PlayerStats.PlantDamage += _PlantDamageIncrement;
            _PlantDamageUpgradeCost += _CostIncrement;
            _txtPlantDamageUpgradeCost.text = _PlantDamageUpgradeCost.ToString();
        }
    }

    //==================== Increase Plant 1 Cap ====================
    [SerializeField] private Text _txtCap1Cost;
    private static int _cap1Cost = 5;
    private int _capIncrement = 2;

    public void BuyCap1Upgrade()
    {
        if (_gamemanager.UseSeeds(_cap1Cost))
        {
            PlayerStats.FriendlyPlant1Cap += _capIncrement;
            _cap1Cost += _CostIncrement;
            _txtCap1Cost.text = _cap1Cost.ToString();
            _UIManager.SetMaxAmountPlant1(PlayerStats.FriendlyPlant1Cap);
            _plantManager.UpdateCap1();
        }
    }

    //==================== Increase Plant 2 Cap ====================
    [SerializeField] private Text _txtCap2Cost;
    private static int _cap2Cost = 5;
   

    public void BuyCap2Upgrade()
    {
        if (_gamemanager.UseSeeds(_cap2Cost))
        {
            PlayerStats.FriendlyPlant2Cap += _capIncrement;
            _cap2Cost += _CostIncrement;
            _txtCap2Cost.text = _cap2Cost.ToString();
            _UIManager.SetMaxAmountPlant2(PlayerStats.FriendlyPlant2Cap);
            _plantManager.UpdateCap2();
        }
    }
    //==================== Increase Plant 3 Cap ====================
    [SerializeField] private Text _txtCap3Cost;
    private static int _cap3Cost = 5;

    public void BuyCap3Upgrade()
    {
        if (_gamemanager.UseSeeds(_cap3Cost))
        {
            PlayerStats.FriendlyPlant3Cap += _capIncrement;
            _cap3Cost += _CostIncrement;
            _txtCap3Cost.text = _cap3Cost.ToString();
            _UIManager.SetMaxAmountPlant3(PlayerStats.FriendlyPlant3Cap);
            _plantManager.UpdateCap3();
        }
    }

    
    private int _HealthIncreaseAmount = 3;

    //==================== Increase Player Max Health ====================
    [SerializeField] private Text _txtPlayerHealthCost;
    private static int _playerHealthCost = 5;

    public void BuyPlayerMaxHealth()
    {
        if (_gamemanager.UseSeeds(_playerHealthCost))
        {
            PlayerStats.PlayerHealth += _HealthIncreaseAmount;
            var player = GameObject.Find("Player");
            var health = player.GetComponent<HealthScript>();

            health.MaxHealth += _HealthIncreaseAmount;
            health.CurrentHealth = health.MaxHealth;

            _playerHealthCost += _CostIncrement;
            _txtPlayerHealthCost.text = _playerHealthCost.ToString();

        }
    }

    //==================== Increase Plants Max Health ====================
    [SerializeField] private Text _txtPlantHealthCost;
    private static int _plantHealthCost = 5;

    public void BuyPlantMaxHealth()
    {
        if (_gamemanager.UseSeeds(_plantHealthCost))
        {
            PlayerStats.PlantHealth += 10.0f;

            _plantHealthCost += _CostIncrement;
            _txtPlantHealthCost.text = _plantHealthCost.ToString();
        }
    }

    private void Start()
    {
        _gamemanager = GameplayManager.Instance;
        _UIManager = UIScript.Instance;
        _plantManager = PlantManager.Instance;


        _txtPlayerDamageUpgradeCost.text = _PlayerDamageUpgradeCost.ToString();
        _txtPlantDamageUpgradeCost.text = _PlantDamageUpgradeCost.ToString();
        _txtCap1Cost.text = _cap1Cost.ToString();
        _txtCap2Cost.text = _cap2Cost.ToString();
        _txtCap3Cost.text = _cap3Cost.ToString();
        _txtPlayerHealthCost.text = _playerHealthCost.ToString();
        _txtPlantHealthCost.text = _plantHealthCost.ToString();
    }

}
