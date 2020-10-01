using UnityEngine;

namespace Core.RT
{
    public class Pickable : Interactable
    {
        public Core.Items.Item item;

        public override void Interact()
        {
            if (RTManager.GetPlayerInventory().AddItem(item))
            {
                Debug.Log("Picked! " + item.title);
                //Destroy(gameObject);
            }
        }
    }
}