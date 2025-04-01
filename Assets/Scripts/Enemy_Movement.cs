using System;
using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{
    public float speed = 2f;
    public bool isChasing = false;
    private int facingDirection = -1; // 1 for right, -1 for left
    
    private Rigidbody2D rb;
    private Transform player;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        if (isChasing == true)
        {
            if (player.position.x > transform.position.x && facingDirection == -1 ||
                player.position.x < transform.position.x && facingDirection == 1)
            {
                Flip();
            }
            Vector2 direction = (player.position - transform.position).normalized;
            rb.linearVelocity = direction * speed;
        }
        
    }
    
    void Flip()
    {
        facingDirection *= -1;
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (player == null)
            {
                player = other.transform;
            }
            isChasing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isChasing = false;
            rb.linearVelocity = Vector2.zero;
        }
    }
}
