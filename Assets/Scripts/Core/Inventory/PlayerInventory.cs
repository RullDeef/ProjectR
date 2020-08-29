using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Inventory
{
    public class PlayerInventory : MonoBehaviour
    {
        public delegate void OnItemsUpdate(PlayerInventory inventory, List<Item> newItems);
        public OnItemsUpdate OnItemsUpdateCallback;

        [SerializeField]
        private List<Item> items;

        public void AddItem(Item item)
        {
            items.Add(item);

            List<Item> newItems = new List<Item>();
            newItems.Add(item);

            if (OnItemsUpdateCallback != null)
                OnItemsUpdateCallback(this, newItems);
        }

        public List<Item> GetItems()
        {
            return items;
        }
    }
}