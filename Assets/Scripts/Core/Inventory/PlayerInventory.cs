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
                UpdateItem(playerItem);
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

        private void UpdateItem(PlayerItem playerItem)
        {
            if (OnItemsUpdateCallback != null)
                OnItemsUpdateCallback(playerItem);
        }

        public void DeleteItem(PlayerItem playerItem)
        {
            MainInventory.Remove(playerItem);
            if (OnItemDeleteCallback != null)
                OnItemDeleteCallback(playerItem);
        }

        private void DeleteItems(List<PlayerItem> itemsToDelete)
        {
            PlayerItem tmp;
            foreach (PlayerItem itemToDelete in itemsToDelete)
            {
                tmp = MainInventory.Find(myItem => myItem.item.id == itemToDelete.item.id && myItem.count >= itemToDelete.count);
                tmp.count -= itemToDelete.count;
                if (tmp.count == 0)
                {
                    DeleteItem(tmp);
                }
                else
                {
                    UpdateItem(tmp);
                }
            }
        }

        public List<PlayerItem> GetInventory()
        {
            return MainInventory;
        }

        public void Craft(List<PlayerItem> ingredients, Item result)
        {
            PlayerItem findIngredient;
            foreach (PlayerItem ingredient in ingredients)
            {
                findIngredient = MainInventory.Find(myItem => myItem.item.id == ingredient.item.id && myItem.count >= ingredient.count);
                if (findIngredient == null) return;
            }
            DeleteItems(ingredients);
            AddItem(result);
        }
    }
}