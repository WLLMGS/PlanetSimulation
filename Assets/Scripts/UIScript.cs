using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{

    private static UIScript _instance = null;

    [SerializeField] private Text _seedCounter;
    [SerializeField] private UISeedDeductionScript _UIDeduction;

    [SerializeField] private Text _currentAmount_p1;
    [SerializeField] private Text _currentAmount_p2;
    [SerializeField] private Text _currentAmount_p3;

    [SerializeField] private Text _maxAmount_p1;
    [SerializeField] private Text _maxAmount_p2;
    [SerializeField] private Text _maxAmount_p3;


    private GameplayManager _gamemanager;

    public static UIScript Instance
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


    void Start()
    {
        _gamemanager = GameplayManager.Instance;
        UpdateUI();
    }

    void Update()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        //update seed counter
        _seedCounter.text = _gamemanager.Seeds.ToString();

    }

    public void ActivateDeductionUI(int amount)
    {
        _UIDeduction.Activate(amount);
    }

    public void SetCurrentAmountPlant1(int amount)
    {
        _currentAmount_p1.text = amount.ToString();
    }
    public void SetCurrentAmountPlant2(int amount)
    {
        _currentAmount_p2.text = amount.ToString();
    }
    public void SetCurrentAmountPlant3(int amount)
    {
        _currentAmount_p3.text = amount.ToString();
    }

    public void SetMaxAmountPlant1(int amount)
    {
        _maxAmount_p1.text = amount.ToString();
    }
    public void SetMaxAmountPlant2(int amount)
    {
        _maxAmount_p2.text = amount.ToString();
    }
    public void SetMaxAmountPlant3(int amount)
    {
        _maxAmount_p3.text = amount.ToString();
    }

}
