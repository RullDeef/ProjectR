using UnityEngine;

namespace Core.RT
{
    public class Pickable : Interactable
    {
        public Inventory.Item item;

        public override void Interact()
        {         
            if (RTManager.GetPlayerInventory().AddItem(item))
            {
                Debug.Log("Picked!");    
                //Destroy(gameObject);
            }         
        }
    }
}