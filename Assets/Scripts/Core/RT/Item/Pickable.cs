using UnityEngine;

namespace Core.RT
{
    public class Pickable : Interactable
    {
        public Inventory.Item item;

        public override void Interact()
        {
            Debug.Log("Picked!");
            RTManager.GetPlayerInventory().AddItem(item);
            //Destroy(gameObject);
        }
    }
}