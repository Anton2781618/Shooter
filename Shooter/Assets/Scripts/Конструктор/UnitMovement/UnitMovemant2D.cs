using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMovemant2D : MonoBehaviour, IUnitMovemant
{
    public bool OnOffLook { get; set; }

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Rigidbody2D rb;
    public Camera cam;
    
    private Vector2 movemant;
    private Vector2 mousePos;

    public void MoveAndLook()
    {
        Move();
        Look();
    }

    private void Move()
    {
        movemant.x = Input.GetAxisRaw("Horizontal");
        movemant.y = Input.GetAxisRaw("Vertical");
        
    }

    private void Look()
    {
        mousePos = Input.mousePosition;
        
        rb.MovePosition(rb.position + movemant * moveSpeed * Time.fixedDeltaTime);
        
        Vector2 looDir = mousePos - rb.position;
        float angle = Mathf.Atan2(looDir.y, looDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }
}
