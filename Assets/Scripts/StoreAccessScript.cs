using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreAccessScript : MonoBehaviour
{

    [SerializeField] public GameObject _tooltip;
    [SerializeField] public GameObject _shop;


    private bool _IsHovering = false;
    private bool _isInShop = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)
        && _IsHovering)
        {
            //access store
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            //disable player movement
            PlayerMovement.CanMove = false;
            PlayerShooting.CanShoot = false;
            HorizontalRotation.CanRotate = false;

            _isInShop = true;

            _tooltip.SetActive(false);

            _shop.SetActive(true);

        }

        if (Input.GetKeyDown(KeyCode.Escape)
        && _isInShop)
        {
            //access store
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            //disable player movement
            PlayerMovement.CanMove = true;
            PlayerShooting.CanShoot = true;
            HorizontalRotation.CanRotate = true;

            _isInShop = false;

            _tooltip.SetActive(true);

            _shop.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            //show tooltip
            _tooltip.SetActive(true);
            _IsHovering = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            //hide tooltip
            _tooltip.SetActive(false);
            _IsHovering = false;
        }
    }
}
