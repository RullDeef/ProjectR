using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core.Inventory
{
    public class PlayerInventory : MonoBehaviour
    {
        public delegate void OnItemsAdd(PlayerItem item);
        public delegate void OnItemUpdate(PlayerItem item);
        public OnItemsAdd OnItemsAddCallback;
        public OnItemUpdate OnItemsUpdateCallback;

        List<PlayerItem> MainInventory;

        private void Awake()
        {
            MainInventory = new List<PlayerItem>();
        }

        public void AddItem(Item item)
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
                playerItem = new PlayerItem(item);
                MainInventory.Add(playerItem);

                if (OnItemsAddCallback != null)
                    OnItemsAddCallback(playerItem);
            }
        }

        public List<PlayerItem> GetInventory()
        {
            return MainInventory;
        }
    }
}