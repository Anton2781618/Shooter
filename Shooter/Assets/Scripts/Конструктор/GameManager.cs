using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Класс управляет сюжетом игры
public class GameManager : MonoBehaviour
{
    public bool isZombie = true;
    public bool StartToAwake = false;
    [SerializeField] private Transform[] spawnPoint;
    [SerializeField] private Transform basePoint;
    [SerializeField] private Transform Zombie;
    
    private Canvas canvas;

    private Coroutine coroutine;
    private void Start()
    {
        canvas = FindObjectOfType<Canvas>();

        if(StartToAwake)
        {
            StartAttack();
        }
    }

    public void StartAttack()
    {
        coroutine = StartCoroutine(TestCoroutine());
    }

    IEnumerator TestCoroutine()
    {
        while(true)
        {
            yield return new WaitForSeconds(1f);
            
            for (int i = 0; i < 5; i++)
            {
                if(isZombie)
                {
                    Transform zombie = Instantiate(Zombie, basePoint.position, Quaternion.identity);
                    zombie.SetParent(canvas.transform);
                }
                else
                {
                    basePoint.transform.rotation = Quaternion.Euler(0, 0, basePoint.transform.rotation.z + Random.Range(10, 100));
                    Transform spider = Instantiate(Zombie, spawnPoint[Random.Range(0, 9)].position, Quaternion.identity);
                    
                    spider.SetParent(canvas.transform);    
                }
                
            }
        }
    }

    public void StopGame()
    {
        StopCoroutine(coroutine); 
    }
}
