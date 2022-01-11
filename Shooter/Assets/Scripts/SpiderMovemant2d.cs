using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderMovemant2d : MonoBehaviour
{
    [SerializeField] private int HP = 100;
    public float speed = 5;
    private Transform target; 
    public Rigidbody2D rb; 

    private Vector2 targetPos;

    private void Start() 
    {
        target = FindObjectOfType<Unit>().transform;    
    }


    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * speed);

        targetPos = target.position;

        if(HP <=0 )Die();
    }

    private void FixedUpdate()
    {
        Vector2 looDir = targetPos - rb.position;
        float angle = Mathf.Atan2(looDir.y, looDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    public void GetHit()
    {
        HP -= 100;
    }
}
