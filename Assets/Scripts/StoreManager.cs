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
    //TODO: add 3rd plant upgrade once plant is implemented



    private void Start()
    {
        _gamemanager = GameplayManager.Instance;
        _UIManager = UIScript.Instance;
        _plantManager = PlantManager.Instance;


        _txtPlayerDamageUpgradeCost.text = _PlayerDamageUpgradeCost.ToString();
        _txtPlantDamageUpgradeCost.text = _PlantDamageUpgradeCost.ToString();
        _txtCap1Cost.text = _cap1Cost.ToString();
        _txtCap2Cost.text = _cap2Cost.ToString();
    }

}
