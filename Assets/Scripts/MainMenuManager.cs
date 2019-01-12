using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuManager : MonoBehaviour
{

    private bool _IsHovering = false;

    private void Update()
    {
        //if mouse is hovering over the button & the mouse is clicked go to first scene
        if (_IsHovering
        && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(0);
        }
    }

    //on mouse enter set hover to true
    private void OnMouseEnter()
    {
        _IsHovering = true;
    }
    //on mouse exit set hover to false
    private void OnMouseExit()
    {
        _IsHovering = false;
    }
}
