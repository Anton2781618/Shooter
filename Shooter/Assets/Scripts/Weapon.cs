using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//класс представляет все ору
public class Weapon : MonoBehaviour, IWeapon
{
    public Transform shootPoint;
    public Transform prefab;

    //патронов в магазине
    private int bulletsPerMag = 30;
    private int bulletsLeft;

    float Firetimer;

    private float nextTimeToFire = 0f;
    
    //скорость стрельбы
    [SerializeField] private float fireRate;

    public void Fire()
    {
        if(Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Debug.Log("fire");    

            Shoot();
            bulletsPerMag--;
        }
    }
    private void Shoot()
    {

        RaycastHit hit;
        if(Physics.Raycast(shootPoint.position, shootPoint.transform.forward, out hit, 100f))
        {
            Instantiate(prefab.gameObject, hit.point, Quaternion.identity);
        }
    }

    public void GiveAway()
    {
        throw new System.NotImplementedException();
    }

    public void Hit()
    {
        throw new System.NotImplementedException();
    }

    public void Inspect()
    {
        throw new System.NotImplementedException();
    }

    public void Modify()
    {
        throw new System.NotImplementedException();
    }

    public void Reload()
    {
        Debug.Log("Reload");
    }

    public void Throw()
    {
        throw new System.NotImplementedException();
    }

    public void Use()
    {
        Fire();
    }

   

   
}
