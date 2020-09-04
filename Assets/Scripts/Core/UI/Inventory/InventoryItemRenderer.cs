using System.Collections.Generic;
using UnityEngine;

// TODO: refactor all namespaces. Separate UI from CORE
namespace UI.Inventory
{
    public class InventoryItemRenderer : MonoBehaviour
    {
        public int cellsCount = 32;
        public InventoryCell emptyCell;
        private List<InventoryCell> cells;
        public Transform _container, _draggingParent;

        private void Start() // подождать Awake у инвентаря плеера
        {
            InitItemCells();
            Core.RTManager.GetPlayerInventory().OnItemsUpdateCallback += UpdateInventoryItems;
            gameObject.SetActive(false); // временный фикс
        }

        private void InitItemCells()
        {
            cells = new List<InventoryCell>();
            InventoryCell tmp;

            for (int i = 0; i < cellsCount; i++)
            {
                tmp = Instantiate(emptyCell, _container);
                tmp.Init(_draggingParent);
                tmp.Render(emptyCell._item);
                cells.Add(tmp);
            }
        }

        // TODO: append new items instead of overwriting from start
        private void UpdateInventoryItems(Core.Inventory.PlayerInventory inventory, List<Core.Inventory.Item> newItems)
        {
            // List<Core.Inventory.Item> items = inventory.GetItems();

            // foreach (Transform child in _container)
            //     Destroy(child.gameObject);

            newItems.ForEach(item =>
            {
                var cell = Instantiate(emptyCell, _container);
                cell.Init(_draggingParent);
                cell.Render(item);
            });
        }
    }
}