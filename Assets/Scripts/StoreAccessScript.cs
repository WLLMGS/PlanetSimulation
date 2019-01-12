using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreAccessScript : MonoBehaviour
{

    [SerializeField] public GameObject _shop;

    private UIScript _UI;

    private bool _IsHovering = false;
    private bool _isInShop = false;

    private void Start()
    {
        _UI = UIScript.Instance;
    }

    private void Update()
    {   
       HandleStoreHandling();
    }

    private void HandleStoreHandling()
    {
        //when E is pressed and player is in range of the store
        //the cursor becomes visible & is no longer locked
        //the player movement, rotation & shooting is disabled
        //bool in shop is set to true
        //the tooltip is enabled
        //and the shop gameobject is set to active
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

            _UI.DisableTooltip();

            _shop.SetActive(true);

        }

        //when R is pressed and the player is currently in the shop
        //set the cursor visiblity to false and lock the cursor
        //enable the player movement, rotation & shooting
        //set bool isInShop to false
        //re-enable the tooltip again
        //set the shop gameobject to false
        if (Input.GetKeyDown(KeyCode.R)
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

            _UI.EnableTooltip("<Press E To Access Store");

            _shop.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //when the player enters the trigger volume set the tooltip to be enabled
        //and set hovering to true
        if (other.tag == "Player")
        {
            //show tooltip
            _UI.EnableTooltip("<Press E To Access Store");
            _IsHovering = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //when the player leaves the trigger volume disable the tooltip
        //and set the hovering bool to false
        if (other.tag == "Player")
        {
            //hide tooltip
            _UI.DisableTooltip();
            _IsHovering = false;
        }
    }
}
