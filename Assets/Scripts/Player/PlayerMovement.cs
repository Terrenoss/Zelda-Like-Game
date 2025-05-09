using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Rigidbody2D rb;
    [SerializeField] private Vector3 moveInput;
    private Animator animator;
    
    private bool isKnockedBack = false;
    
    public Player_Combat playerCombat;
    
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
    
    public void Attack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            playerCombat.Attack();
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        animator.SetBool("isWalking", true);

        if (context.canceled)
        {
            animator.SetBool("isWalking", false);
            animator.SetFloat("LastInputX", moveInput.x);
            animator.SetFloat("LastInputY", moveInput.y);
        }
        
        moveInput = context.ReadValue<Vector2>();
        
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
