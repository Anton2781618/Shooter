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
    //скорость стрельбы
    [SerializeField] private float fireRate;
    

    [Header("Настройки прицелевания")]
    private WeaponAim weaponAim;
    [SerializeField] private Vector3 aimPosition;

    [Header("Настройки звука")]
    [SerializeField] private List <AudioClip> sounds = new List<AudioClip>();

    [Header("Настройки UI")]
    [SerializeField] private TextMeshProUGUI Lable;

    private WeaponSway weaponSway;
    [SerializeField] private WeaponClip weaponClip;

    private Animator animator; 
    private AudioSource audioSource; 

    private float nextTimeToFire = 0f;
    private bool isReload = false;   

    private void Awake() 
    {
        weaponAim = new WeaponAim(this, aimPosition);
        weaponSway = GetComponent<WeaponSway>();

        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        Lable.text = weaponClip.ShowAmmo();
        Debug.Log("OnEnable");    
    }

    public void Fire()
    {
        if(Time.time < nextTimeToFire || isReload) return;

        if(weaponClip.GetCurrentBullets() > 0)
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
        weaponClip.SubtractAmmo();

        Lable.text = weaponClip.ShowAmmo();
        
        muzzleFlash.Play();
        muzzleFlash.transform.Rotate(Random.Range(-50,50), 0, 0);
        
        animator.CrossFadeInFixedTime("Fire", 0.1f);        

        RaycastHit hit;
        if(Physics.Raycast(shootPoint.position, shootPoint.transform.forward, out hit, 100f))
        {
            GameObject HitParticleEffect = Instantiate(Bullethols.gameObject, hit.point,
            
            Quaternion.FromToRotation(Vector3.forward, hit.normal));
        }
    }
//-0.123 1.5 0.125
    
    public void Aim()
    {
        weaponSway.enabled = false;
        weaponAim.Aim();
        weaponSway.enabled = true;
    }

    public void Reload()
    {
        AnimatorStateInfo info = animator.GetCurrentAnimatorStateInfo(0);

        if(info.IsName("Reload")) return;
        audioSource.PlayOneShot(sounds[1]);

        isReload = true;
        animator.CrossFadeInFixedTime("Reload", 0.01f);
    }

    private void DoReload()
    {
        if(weaponClip.bulletsLeft <= 0) return;

        weaponClip.CalculateAmmo();
        Lable.text = weaponClip.ShowAmmo();
        isReload = false;
    }

    public void Throw()
    {
        Debug.Log("выбросил оружие");
    }

    public void Use()
    {
        Fire();
    }

    public void Take()
    {        
        Debug.Log(animator);
        animator.CrossFadeInFixedTime("Draw", 0.01f);
        Debug.Log("взял оружие");
    }
}
