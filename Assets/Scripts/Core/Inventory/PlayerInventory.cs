using System.Collections.Generic;
using UnityEngine;

namespace Core.Inventory
{
    public class PlayerInventory : MonoBehaviour
    {
        public delegate void OnItemsAdd(PlayerItem item);
        public delegate void OnItemUpdate(PlayerItem item);
        public delegate void OnItemDelete(PlayerItem item);
        public OnItemsAdd OnItemsAddCallback;
        public OnItemUpdate OnItemsUpdateCallback;
        public static OnItemDelete OnItemDeleteCallback;

        List<PlayerItem> MainInventory;
        public int maxItems;
        public static PlayerInventory instance;

        private void Awake()
        {
            instance = this;
            MainInventory = new List<PlayerItem>();
        }

        public bool AddItem(Item item)
        {
            // найти, чтобы положить в стак, если предмет есть и его можно застакать
            PlayerItem playerItem = MainInventory.Find(_ => _.item.id == item.id && _.count < item.maxStacks);
            if (playerItem != null)
            {
                playerItem.count++;
                if (OnItemsUpdateCallback != null)
                    OnItemsUpdateCallback(playerItem);
            }
            else
            {
                if (MainInventory.Count >= maxItems) return false;

                playerItem = new PlayerItem(item);
                MainInventory.Add(playerItem);
                if (OnItemsAddCallback != null)
                    OnItemsAddCallback(playerItem);
            }

            return true;
        }

        public void DeleteItem(PlayerItem playerItem)
        {
            PlayerItem itemToDelete = MainInventory.Find(item => item.Equals(playerItem));
            MainInventory.Remove(itemToDelete);
            if (OnItemDeleteCallback != null)
                OnItemDeleteCallback(itemToDelete);
        }

        public List<PlayerItem> GetInventory()
        {
            return MainInventory;
        }
    }
}