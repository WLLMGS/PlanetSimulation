using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpeningScript : MonoBehaviour
{
    private UIScript _UI;
    private Animator _animator;
    private bool _isInRange = false;
    private bool _IsOpened = false;
    private void Start()
    {
        _UI = UIScript.Instance;
        _animator = GetComponentInChildren<Animator>();
        _animator.speed = 0.0f;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) 
        && _isInRange
            && !_IsOpened)
        {
            _IsOpened = true;
            _animator.speed = 1.0f;
            _animator.Play(0);
            _UI.DisableTooltip();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player"
            && !_IsOpened)
        {
            _isInRange = true;
            _UI.EnableTooltip("<Press E To Open Door");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player"
            && !_IsOpened)
        {
            _isInRange = false;
            _UI.DisableTooltip();
        }
    }
}
