using System.Collections.Generic;
using System.Linq;
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

        public bool AddItem(PlayerItem itemToAdd)
        {
            PlayerItem existingPlayerItem = MainInventory.Find(_ => _.item.id == itemToAdd.item.id && _.count < _.item.maxStacks);

            if (existingPlayerItem != null)
            {
                int availableSpace = itemToAdd.item.maxStacks - existingPlayerItem.count;
                if (availableSpace >= itemToAdd.count)
                {
                    existingPlayerItem.count += itemToAdd.count;
                }
                else // Если места в существующем итеме не хватит
                {
                    if (MainInventory.Count >= maxItems) return false;

                    existingPlayerItem.count += availableSpace;
                    PlayerItem newItem = new PlayerItem(itemToAdd.item, itemToAdd.count - availableSpace);
                    MainInventory.Add(newItem);

                    if (OnItemsAddCallback != null)
                        OnItemsAddCallback(newItem);
                }
                UpdateItem(existingPlayerItem);
            }
            else
            {
                if (MainInventory.Count >= maxItems) return false;

                PlayerItem newItem = new PlayerItem(itemToAdd.item, itemToAdd.count);
                MainInventory.Add(newItem);

                if (OnItemsAddCallback != null)
                    OnItemsAddCallback(newItem);
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
            PlayerItem findItem;
            foreach (PlayerItem itemToDelete in itemsToDelete)
            {
                findItem = MainInventory.Find(myItem => myItem.item.id == itemToDelete.item.id && myItem.count >= itemToDelete.count);
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
            return MainInventory;
        }

        public bool Craft(List<PlayerItem> ingredients, PlayerItem result)
        {
            // Ищем ингредиенты, если нету хотя бы одного, то не крафтим
            List<PlayerItem> find = MainInventory.Where(myItem => ingredients.All(ingredient => myItem.item.id == ingredient.item.id && myItem.count >= ingredient.count)).ToList();
            if (find.Count >= ingredients.Count) // пока так, если нашлись все предметы
            {
                DeleteItems(ingredients);
                AddItem(result);
                return true;
            }

            return false;
        }
    }
}