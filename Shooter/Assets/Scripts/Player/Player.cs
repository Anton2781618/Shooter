using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    public static Player playerSingleton;

    
    //руки игрока в них может быть все что можно удерживать  
    private Iholding heands;
    private IInvent inventory;

    private bool inventoryIsOpen;
    private WeaponSway weaponSway;
    public LookWithMouse mouseLook;

    private void Awake() 
    {
        inventory = GetComponent<IInvent>();
        weaponSway = new WeaponSway();

        mouseLook = Camera.main.GetComponent<LookWithMouse>();

        playerSingleton = this;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        InputSystem();
    }
    
    // взять в руки
    public void TakeInHends(Iholding predmet)
    {
        heands = predmet;
        heands.Take();
    }

    private void InputSystem()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            inventoryIsOpen = inventory.OpenCloseInventory();
            mouseLook.enabled = !mouseLook.enabled;
            Cursor.lockState = inventoryIsOpen ? CursorLockMode.Confined : CursorLockMode.Locked;
        }

        if(Input.GetKeyDown(KeyCode.E))
        {
            CheckRay();
        }  

        if(heands == null || inventoryIsOpen) return;

        if(Input.GetKey(KeyCode.Mouse0))
        {
            heands.Use();
        }

        if(Input.GetKey(KeyCode.Mouse1))
        {
            Weapon weapon = (Weapon)heands;
            weapon.Aim();
        }

        if(Input.GetKeyDown(KeyCode.R) && heands is Weapon)
        {
            Weapon weapon = (Weapon)heands;
            weapon.Reload();
        }

        heands.Sway();      
    }

    private void CheckRay()
    {
        var ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0));
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, 100f))
        {
            if(hit.transform.GetComponent<ItemWorld>())
            {
                inventory.AddItem(hit.transform.GetComponent<ItemWorld>().Grab());
            }
        }
    }
}
