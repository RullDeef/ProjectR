using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// TODO: refactor all namespaces. Separate UI from CORE
namespace UI.Inventory
{
    public class InventoryItemRenderer : MonoBehaviour, IPointerDownHandler
    {
        public int cellsCount;
        public GameObject emptySlot, target;
        InventoryCell newCell;
        private List<InventoryCell> cells;
        public Transform _container, _draggingParent;
        public Text descriptionText;

        private void Start() // подождать Awake у инвентаря плеера
        {
            InitItemCells();
            Core.RTManager.GetPlayerInventory().OnItemsAddCallback += AddInventoryItem;
            Core.RTManager.GetPlayerInventory().OnItemsUpdateCallback += UpdateInventoryItem;
            gameObject.SetActive(false); // временный фикс
        }

        private void InitItemCells()
        {
            cells = new List<InventoryCell>();
            newCell = emptySlot.GetComponent<InventoryCell>();
            InventoryCell tmp;

            for (int i = 0; i < cellsCount; i++)
            {
                tmp = Instantiate(newCell, _container);
                tmp.Init(newCell._playerItem, _draggingParent);
                tmp.Render();
                cells.Add(tmp);
            }
        }

        // TODO: append new items instead of overwriting from start
        private void AddInventoryItem(Core.Inventory.PlayerItem newItem)
        {
            var cell = Instantiate(newCell, _container);
            //inventory.SetIndex(newItem, cell.transform.GetSiblingIndex());
            cell.Init(newItem, _draggingParent);
            cell.Render();

            cells.Add(cell);
        }

        private void UpdateInventoryItem(Core.Inventory.PlayerItem playerItem)
        {
            var cell = cells.Find(inventoryCell => inventoryCell._playerItem.Equals(playerItem));
            cell.RenderText();
        }

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            var cell = cells.Find(cel => RectTransformUtility.RectangleContainsScreenPoint(cel.rectTransform, eventData.position));
            if (cell != null)
            {
                descriptionText.text = cell._playerItem.ToString();
                target.transform.position = cell.transform.position;
                target.transform.SetParent(cell.transform);
            }
            else
            {
                descriptionText.text = "";
                target.transform.position = new Vector2(-50, -50);
                target.transform.SetParent(_container.parent);
            }
        }
    }
}