using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//класс представляет все ору
public class Weapon : MonoBehaviour, Iholding, IFirearms
{
    [Header("Настройки оружия")]
    //точка вспышки
    [SerializeField] private Transform shootPoint;
    [SerializeField] private ParticleSystem muzzleFlash;
    
    [SerializeField] private GameObject Bullethols;

    [SerializeField] private List <AudioClip> sounds = new List<AudioClip>(); 

    //скорость стрельбы
    [SerializeField] private float fireRate;
    [SerializeField] private TextMeshProUGUI Lable;

    [Header("Настройки прицелевания")]
    private Vector3 origPosition;
    [SerializeField] private Vector3 AimPosition;

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
        origPosition = transform.localPosition;

        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        ShowAmmo();
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
        ShowAmmo();
        muzzleFlash.Play();
        muzzleFlash.transform.Rotate(Random.Range(-50,50), 0, 0);
        anim.CrossFadeInFixedTime("Fire", 0.1f);        

        RaycastHit hit;
        if(Physics.Raycast(shootPoint.position, shootPoint.transform.forward, out hit, 100f))
        {
            // GameObject HitParticleEffect = Instantiate(prefab.gameObject, hit.point, hit.normal);
            GameObject HitParticleEffect = Instantiate(Bullethols.gameObject, hit.point,
            
            Quaternion.FromToRotation(Vector3.forward, hit.normal));
            
        }
    }
//-0.123 0.125
    public WeaponSway weaponSway;
    public void Aim()
    {
        weaponSway.enabled = false;
        transform.localPosition = Vector3.Lerp(transform.localPosition, AimPosition, Time.deltaTime * 10);
    }

    public void NoAim()
    {
        weaponSway.enabled = true;
        transform.localPosition = Vector3.Lerp(transform.localPosition, origPosition, Time.deltaTime * 10);
    }

    private void ShowAmmo()
    {
        Lable.text = currentBullets.ToString() + " / " + bulletsLeft.ToString();
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
        ShowAmmo();
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

    public void Take()
    {
        throw new System.NotImplementedException();
    }
}
