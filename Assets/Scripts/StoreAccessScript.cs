using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreAccessScript : MonoBehaviour
{

    [SerializeField] public GameObject _shop;

    private UIScript _UI;

    private bool _IsHovering = false;
    private bool _isInShop = false;

    private void Awake()
    {
        //DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        _UI = UIScript.Instance;
    }

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

            _UI.EnableTooltip("<Press E To Access Store");

            _shop.SetActive(true);

        }
        
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
        if (other.tag == "Player")
        {
            //show tooltip
            _UI.EnableTooltip("<Press E To Access Store");
            _IsHovering = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            //hide tooltip
            _UI.DisableTooltip();
            _IsHovering = false;
        }
    }
}
