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

    //activates the deduction game object
    //and invokes the deactivate function in _cooldown amount of seconds
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
