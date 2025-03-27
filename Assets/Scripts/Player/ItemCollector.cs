using Unity.VisualScripting;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    
    private InventoryController inventoryController;
    
    void Start()
    {
        inventoryController = FindObjectOfType<InventoryController>();
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("InventoryItems"))
            return;
        
        if(collision.CompareTag("Item"))
        {
            Item item = collision.GetComponent<Item>();
            
            if(item != null)
            {
                bool itemAdded = inventoryController.AddItem(collision .gameObject);
                
                if(itemAdded)
                {
                    item.PickUp();
                    Destroy(collision.gameObject);
                }
            }
        }
    }
}
