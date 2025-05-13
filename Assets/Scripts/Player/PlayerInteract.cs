using Interact;
using System;
using System.Collections;
using UnityEngine;

namespace Player
{
    public class PlayerInteract : MonoBehaviour
    {
        [SerializeField] private float interactionDistance = 2f;
        [SerializeField] private LayerMask interactableLayer;
        [SerializeField] private float checkForInteractableDelay = 0.5f;
        
        IInteractable currentInteractable;

        private void Start()
        {
            StartCoroutine(CheckForInteractables());
        }
        public void Interact()
        {
            GetNearestInteractable()?.Interact();
        }
        
        
        private IInteractable GetNearestInteractable()
        {
            Collider2D[] objectInRange = Physics2D.OverlapCircleAll(transform.position, interactionDistance, interactableLayer);

            if (objectInRange.Length <= 0)
                return null;
            
            float closestDistance = Mathf.Infinity;
            IInteractable closestInteractable = null;
                
            for (int i = 1; i < objectInRange.Length; i++)
            {
                Collider2D obj = objectInRange[i];

                if (!obj.TryGetComponent(out IInteractable interactable))
                    continue;
                
                float distance = Vector2.Distance(transform.position, obj.transform.position);

                if (distance > closestDistance)
                    continue;
                
                closestDistance = distance;
                closestInteractable = interactable;
            }

            return closestInteractable;
        }
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, interactionDistance);
        }

        private IEnumerator CheckForInteractables()
        {
            while (true)
            {
                currentInteractable?.HideInteractableUI();
                currentInteractable = GetNearestInteractable();
                currentInteractable?.ShowInteractableUI();
                yield return new WaitForSeconds(checkForInteractableDelay);
            }
        }
    }
}
