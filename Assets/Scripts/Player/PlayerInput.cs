using Player;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerCombat playerCombat;
    [SerializeField] private PlayerInteract playerInteract;
    
    
    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerCombat = GetComponent<PlayerCombat>();
        playerInteract = GetComponent<PlayerInteract>();
    }
    
    public void Attack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            playerCombat.Attack();
        }
    }
    
    public void Interact(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            playerInteract.Interact();
        }
    }
    
    public void Move(InputAction.CallbackContext context)
    {
        playerMovement.Move(context.ReadValue<Vector2>());
    }
}
