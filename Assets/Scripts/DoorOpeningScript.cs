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
        //get UI manager
        _UI = UIScript.Instance;
        //get the animator
        _animator = GetComponentInChildren<Animator>();
        //set current animator speed to zero
        _animator.speed = 0.0f;
    }

    private void Update()
    {
        //if player is in range
        //and E is pressed
        //and the door isnt opened already
        //play the animation & disable the tool tip
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
        //if player enters the triggerbox & the door is unopened set inrange bool to true
        //and activate the tooltip
        if (other.tag == "Player"
            && !_IsOpened)
        {
            _isInRange = true;
            _UI.EnableTooltip("<Press E To Open Door");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //if player exits the triggerbox & the door is unopened set the inrange bool to false
        //and disable the tooltip
        if (other.tag == "Player"
            && !_IsOpened)
        {
            _isInRange = false;
            _UI.DisableTooltip();
        }
    }
}
