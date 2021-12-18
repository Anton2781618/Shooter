using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//класс представляет все ору
public class Weapon : MonoBehaviour, IWeapon
{
    //точка вспышки
    [SerializeField] private Transform shootPoint;
    [SerializeField] private ParticleSystem muzzleFlash;

    [SerializeField] private Transform prefab;
    [SerializeField] private List <AudioClip> sounds = new List<AudioClip>(); 

    //скорость стрельбы
    [SerializeField] private float fireRate;

    //патронов в магазине
    public int bulletsPerMag = 200;
    public int currentBullets = 30;
    public int bulletsLeft;
    
    private Animator anim; 
    private AudioSource audioSource; 

    private float nextTimeToFire = 0f;
    public bool isReload = true;

    private void Start() 
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    public void Fire()
    {
        if(Time.time < nextTimeToFire || !isReload) return;
        
        if(currentBullets > 0)
        {
            Shoot();
            
        }
        else
        {
            Reload();
        }
        nextTimeToFire = Time.time + 1f / fireRate;
    }

    private void Shoot()
    {
        audioSource.PlayOneShot(sounds[0]);
        currentBullets--;
        muzzleFlash.Play();
        anim.CrossFadeInFixedTime("Fire", 0.1f);        

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
        AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);

        if(info.IsName("Reload")) return;
        audioSource.PlayOneShot(sounds[1]);

        isReload = false;
        anim.CrossFadeInFixedTime("Reload", 0.01f);
    }

    private void DoReload()
    {
        if(bulletsLeft <= 0) return;

        int buletsToLoad = bulletsPerMag - currentBullets;
        int bulletsToDeduct = (bulletsLeft >= buletsToLoad) ? buletsToLoad : bulletsLeft;

        bulletsLeft -= bulletsToDeduct;
        currentBullets += bulletsToDeduct;

        isReload = true;
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
