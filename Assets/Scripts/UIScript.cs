using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{

    private static UIScript _instance = null;

    [SerializeField] private Text _seedCounter;
    [SerializeField] private UISeedDeductionScript _UIDeduction;

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
}
