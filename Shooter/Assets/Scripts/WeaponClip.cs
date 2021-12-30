using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponClip : MonoBehaviour
{
    //сколько патронов вмещается в обойму
    public int bulletsPerMag;
    //сколько патронов сейчас есть в обойме
    public int currentBullets;
    //всего сейчас патронов в инвентаре
    public int bulletsLeft;

    public string ShowAmmo()
    {
        return currentBullets.ToString() + " / " + bulletsLeft.ToString();
    }

    public int GetCurrentBullets()
    {
        return currentBullets;
    } 

    //просто вычитает патрон
    public void SubtractAmmo()
    {
        currentBullets--;
    }

    public void CalculateAmmo()
    {
        int buletsToLoad = bulletsPerMag - currentBullets;
        int bulletsToDeduct = (bulletsLeft >= buletsToLoad) ? buletsToLoad : bulletsLeft;

        bulletsLeft -= bulletsToDeduct;
        currentBullets += bulletsToDeduct;
    }

}
