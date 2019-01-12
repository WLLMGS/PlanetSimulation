using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitButton : MonoBehaviour {

    private bool _IsHovering = false;

    private Text _text;

    private void Start()
    {
        _text = GetComponentInChildren<Text>();
    }

    private void Update()
    {
        //if hovering & mouse button clicked quit the game
        if (_IsHovering
            && Input.GetMouseButtonDown(0))
        {
            Application.Quit();
        }
    }
    //when mouse entered set hovering to true and color to grey
    private void OnMouseEnter()
    {
        _IsHovering = true;
        _text.color = Color.gray;
    }
    //when mouse left set hovering to false and color to white
    private void OnMouseExit()
    {
        _IsHovering = false;
        _text.color = Color.white;
    }
}
