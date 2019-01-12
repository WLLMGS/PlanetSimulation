using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartButtonV2 : MonoBehaviour
{
    private bool _IsHovering = false;
    private Text _text;


    private void Start()
    {
        _text = GetComponentInChildren<Text>();
    }

    private void Update()
    {
        //if is hovering & mouse is clicked go to scene 1
        if (_IsHovering
            && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(1);
        }
    }

    //on mouse enter set hovering to true & set color to gray
    private void OnMouseEnter()
    {
        _IsHovering = true;
        _text.color = Color.gray;
    }
    //on mouse exit set hovering to false & set color to white
    private void OnMouseExit()
    {
        _IsHovering = false;
        _text.color = Color.white;
    }
}
