using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Core.Items;

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

        List<PlayerItem> mainInventory;
        public int maxItems;
        public static PlayerInventory instance;

        private void Awake()
        {
            instance = this;
            mainInventory = new List<PlayerItem>();
        }

        public bool AddItem(Item item)
        {
            // найти, чтобы положить в стак, если предмет есть и его можно застакать
            PlayerItem playerItem = mainInventory.Find(_ => _.item.id == item.id && _.count < item.maxStacks);
            if (playerItem != null)
            {
                playerItem.count++;
                UpdateItem(playerItem);
            }
            else
            {
                if (mainInventory.Count >= maxItems) return false;

                playerItem = new PlayerItem(item);
                mainInventory.Add(playerItem);
                if (OnItemsAddCallback != null)
                    OnItemsAddCallback(playerItem);
            }

            return true;
        }

        public bool AddItem(PlayerItem itemToAdd)
        {
            PlayerItem existingPlayerItem = mainInventory.Find(_ => _.item.id == itemToAdd.item.id && _.count < _.item.maxStacks);

            if (existingPlayerItem != null)
            {
                int availableSpace = itemToAdd.item.maxStacks - existingPlayerItem.count;
                if (availableSpace >= itemToAdd.count)
                {
                    existingPlayerItem.count += itemToAdd.count;
                }
                else // Если места в существующем итеме не хватит
                {
                    if (mainInventory.Count >= maxItems) return false;

                    existingPlayerItem.count += availableSpace;
                    PlayerItem newItem = new PlayerItem(itemToAdd.item, itemToAdd.count - availableSpace);
                    mainInventory.Add(newItem);

                    if (OnItemsAddCallback != null)
                        OnItemsAddCallback(newItem);
                }
                UpdateItem(existingPlayerItem);
            }
            else
            {
                if (mainInventory.Count >= maxItems) return false;

                PlayerItem newItem = new PlayerItem(itemToAdd.item, itemToAdd.count);
                mainInventory.Add(newItem);

                if (OnItemsAddCallback != null)
                    OnItemsAddCallback(newItem);
            }

            return true;
        }

        public void UpdateItem(PlayerItem playerItem)
        {
            if (OnItemsUpdateCallback != null)
                OnItemsUpdateCallback(playerItem);
        }

        public void DeleteItem(PlayerItem playerItem)
        {
            mainInventory.Remove(playerItem);
            if (OnItemDeleteCallback != null)
                OnItemDeleteCallback(playerItem);
        }

        private void DeleteItems(List<PlayerItem> itemsToDelete)
        {
            PlayerItem findItem;
            foreach (PlayerItem itemToDelete in itemsToDelete)
            {
                findItem = mainInventory.Find(myItem => myItem.item.id == itemToDelete.item.id && myItem.count >= itemToDelete.count);
                findItem.count -= itemToDelete.count;
                if (findItem.count == 0)
                {
                    DeleteItem(findItem);
                }
                else
                {
                    UpdateItem(findItem);
                }
            }
        }

        public List<PlayerItem> GetInventory()
        {
            return mainInventory;
        }

        public bool Craft(List<PlayerItem> ingredients, PlayerItem result)
        {
            // Ищем ингредиенты, если нету хотя бы одного, то не крафтим
            List<PlayerItem> find = mainInventory.Where(myItem => ingredients.Any(ingredient => myItem.item.id == ingredient.item.id && myItem.count >= ingredient.count)).ToList();
            if (find.Count >= ingredients.Count) // пока так, если нашлись все предметы
            {
                DeleteItems(ingredients);
                AddItem(result);
                return true;
            }

            return false;
        }

        // public void UseItem(PlayerItem itemUse)
        // {
        //     if (itemUse.Use())
        //     {
        //         itemUse.count--;
        //         if (itemUse.count == 0)
        //         {
        //             DeleteItem(itemUse);
        //         }
        //         else
        //         {
        //             UpdateItem(itemUse);
        //         }
        //     }
        // }
    }
}