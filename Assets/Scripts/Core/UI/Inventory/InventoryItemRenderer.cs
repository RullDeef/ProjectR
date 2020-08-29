using System.Collections.Generic;
using UnityEngine;

// TODO: refactor all namespaces. Separate UI from CORE
namespace UI.Inventory
{
    public class InventoryItemRenderer : MonoBehaviour
    {
        public Vector2Int cellsCount = new Vector2Int(8, 6);
        public Vector2Int startingOffset = new Vector2Int(16, 16);
        public Vector2Int spacing = new Vector2Int(8, 8);

        public GameObject emptyCell;

        private GameObject[,] cells;

        private void Awake()
        {
            InitItemCells();
            Core.RTManager.GetPlayerInventory().OnItemsUpdateCallback += UpdateInventoryItems;
        }

        private void InitItemCells()
        {
            cells = new GameObject[cellsCount.x, cellsCount.y];

            for (int row = 0; row < cellsCount.x; row++)
            for (int col = 0; col < cellsCount.y; col++)
            {
                // TODO: hardcoded cell size (64, 64)
                Vector3 pos = new Vector3(startingOffset.x + row * (64 + spacing.x), -(startingOffset.y + col * (64 + spacing.y)), 0);
                cells[row, col] = Instantiate(emptyCell, pos, Quaternion.identity, transform);
                // cells[row, col].transform.SetParent(transform, false);
                cells[row, col].GetComponent<RectTransform>().anchoredPosition = pos;
            }
        }

        // TODO: append new items instead of overwriting from start
        private void UpdateInventoryItems(Core.Inventory.PlayerInventory inventory, List<Core.Inventory.Item> newItems)
        {
            List<Core.Inventory.Item> items = inventory.GetItems();
            int itemIndex = 0; // in inventory list
            
            for (int row = 0; row < cellsCount.x; row++)
            for (int col = 0; col < cellsCount.y; col++)
            {
                if (itemIndex == items.Count)
                    return;

                Core.Inventory.Item item = items[itemIndex];

                // update inventory icon here
                cells[row, col].GetComponent<UnityEngine.UI.RawImage>().texture = item.slotIcon;

                itemIndex++;
            }
        }
    }
}