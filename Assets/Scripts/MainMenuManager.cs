using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenuManager : MonoBehaviour
{

    private bool _IsHovering = false;

    private void Update()
    {
        if (_IsHovering
        && Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene(0);
        }
    }

    private void OnMouseEnter()
    {
        _IsHovering = true;
    }

    private void OnMouseExit()
    {
        _IsHovering = false;
    }
}
