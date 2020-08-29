using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Inventory
{
    public class PlayerInventory : MonoBehaviour
    {
        [SerializeField]
        private List<Item> items;

        public void AddItem(Item item)
        {
            items.Add(item);
        }
    }
}