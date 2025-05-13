using UnityEngine;

namespace Interact
{
    public class CampFire : MonoBehaviour, IInteractable
    {
        [SerializeField] private GameObject interactableUI;
        [SerializeField] private GameObject campFirePanel;
        public void Interact()
        {
            campFirePanel.SetActive(!campFirePanel.activeSelf);
        }
        public void ShowInteractableUI()
        {
            interactableUI.SetActive(true);
        }
        public void HideInteractableUI()
        {
            interactableUI.SetActive(false);
        }
    }
}
