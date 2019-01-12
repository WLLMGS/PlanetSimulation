using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{

    private static UIScript _instance = null;

    [SerializeField] private Text _seedCounter;
    [SerializeField] private UISeedDeductionScript _UIDeduction;

    [SerializeField] private GameObject _tooltip;
    [SerializeField] private Text _txtObjective;

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
        DontDestroyOnLoad(gameObject); //dont destroy the canvas when loading a new scene

        if (_instance == null) _instance = this; //create a new instance if its NULL
    }


    void Start()
    {
        _gamemanager = GameplayManager.Instance;
        UpdateUI(); //update UI on start
    }

    void Update()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        //update seed counter
        _seedCounter.text = _gamemanager.Seeds.ToString(); //set the seed counter to the current amount of seeds the player has

    }

    //activates the red deducation text
    public void ActivateDeductionUI(int amount)
    {
        _UIDeduction.Activate(amount);
    }
    //sets the current amount of plant 1 in the HUD
    public void SetCurrentAmountPlant1(int amount)
    {
        _currentAmount_p1.text = amount.ToString();
    }
    //sets the current amount of plant 2 in the HUD
    public void SetCurrentAmountPlant2(int amount)
    {
        _currentAmount_p2.text = amount.ToString();
    }
    //sets the current amount of plant 3 in the HUD
    public void SetCurrentAmountPlant3(int amount)
    {
        _currentAmount_p3.text = amount.ToString();
    }

    //sets the max amount of plant 1 in the HUD
    public void SetMaxAmountPlant1(int amount)
    {
        _maxAmount_p1.text = amount.ToString();
    }
    //sets the max amount of plant 2 in the HUD
    public void SetMaxAmountPlant2(int amount)
    {
        _maxAmount_p2.text = amount.ToString();
    }
    //sets the max amount of plant 3 in the HUD
    public void SetMaxAmountPlant3(int amount)
    {
        _maxAmount_p3.text = amount.ToString();
    }
    //disable the tooltip
    public void DisableTooltip()
    {
        _tooltip.SetActive(false);
    }
    //enable the tooltip with a custom message
    public void EnableTooltip(string message)
    {
        _tooltip.GetComponent<Text>().text = message;
        _tooltip.SetActive(true);
    }

    //sets the current objective in the HUD to the string
    public void SetObjective(string message)
    {
        _txtObjective.text = message;
    }

}
