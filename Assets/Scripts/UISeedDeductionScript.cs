using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISeedDeductionScript : MonoBehaviour {

    private Text _text;
    private float _cooldown = 2.0f;

    private void Start()
    {
        _text = GetComponent<Text>();
    }

    public void Activate(int amount)
    {
        _text.text = "- " + amount.ToString();
        Invoke("Deactivate", _cooldown);
    }

    private void Deactivate()
    {
        _text.text = "";
    }

}
