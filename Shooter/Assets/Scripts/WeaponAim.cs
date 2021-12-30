using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAim 
{
    private Weapon weapon;
    private Vector3 origPosition;
    private Vector3 aimPosition;

    private Coroutine currentCoroutine;

    public WeaponAim(Weapon weapon, Vector3 aimPosition)
    {
        this.weapon = weapon;
        this.aimPosition = aimPosition;

        origPosition = weapon.transform.localPosition;
    }
    
    public void Aim()
    {
        weapon.transform.localPosition = 
        Vector3.Lerp(weapon.transform.localPosition, aimPosition, Time.deltaTime * 10);
    }
}
