using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private Vector3 moveInput;
    private Animator animator;
    
    private bool isKnockedBack = false;
    
    public PlayerCombat playerCombat;
    
    [Header("Movement Stats")]
    public float moveSpeed = 5f;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator  = GetComponent<Animator>();
    }

    
    void Update()
    {
        
        if (isKnockedBack == false)
        {
            rb.linearVelocity = moveInput * moveSpeed;
        }
    }

    public void Move(Vector2 direction)
    {
        animator.SetBool("isWalking", true);
        
        if (direction == Vector2.zero)
        {
            animator.SetBool("isWalking", false);
            animator.SetFloat("LastInputX", moveInput.x);
            animator.SetFloat("LastInputY", moveInput.y);
        }
        
        moveInput = direction;
        
        animator.SetFloat("InputX", moveInput.x);
        animator.SetFloat("InputY", moveInput.y);
    }
    
    public void Knockback(Transform objt, float force, float stunTime)
    {
        isKnockedBack = true;
        Vector3 direction = (transform.position - objt.position).normalized;
        rb.linearVelocity = direction * force;
        StartCoroutine(KnockBackCounter(stunTime));
    }

    IEnumerator KnockBackCounter(float stunTime)
    {
        yield return new WaitForSeconds(stunTime);
        rb.linearVelocity = Vector2.zero;
        isKnockedBack = false;
    }
}
