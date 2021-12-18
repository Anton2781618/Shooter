using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Player : MonoBehaviour
{
    //руки игрока в них может быть все что можно удерживать  
    private Iholding heands;

    private void Start() 
    {

        heands = FindObjectOfType<Weapon>();
        // heands = med;
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

        if(Input.GetKeyDown(KeyCode.R) && heands is Weapon)
        {
            Weapon weapon = (Weapon)heands;
            weapon.Reload();
        }
    }
}
