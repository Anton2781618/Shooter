using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private int HP = 100;
    public GameObject Bullethols;
    private NavMeshAgent agent;
    private Animator animator;

    private void Start() 
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        // target = fin
    }

    void Update()
    {
        States();
    }

    private void States()
    {
        float distance = Vector3.Distance(target.position , transform.position);
        if(distance <= agent.stoppingDistance )
        {
            
            Attack();
        }
        else
        {
            Move();
        }
    }

    public void GetHit()
    {
        HP -= 20;
        if(HP <= 0 )
        {
            Destroy(gameObject);    
        }
    }

    private void Attack()
    {
        animator.SetBool("Run", false);
        animator.SetBool("Attack", true);
    }

    public void Hit()
    {
        target.GetComponent<Player>().GetHit();
    }

    private void Move()
    {
        agent.SetDestination(target.transform.position);

        animator.SetBool("Attack", false);
        animator.SetBool("Run", true);

    }
}
