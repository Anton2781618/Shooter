using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    //руки игрока в них может быть все что можно удерживать  
    private Iholding heands;
    [SerializeField] private Inventory inventory;


    private void Start() 
    {
        heands = FindObjectOfType<Weapon>();
        
    }

    void Update()
    {
        InputSystem();
    }

    private void InputSystem()
    {
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

        if(Input.GetKeyDown(KeyCode.E))
        {
            // heands.Take();
        }

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            heands = inventory.GetItem(0);
            heands.Take();
        }

        if(Input.GetKeyDown(KeyCode.Alpha2))
        {
            heands = inventory.GetItem(1);
            heands.Take();
        }
    }
}
