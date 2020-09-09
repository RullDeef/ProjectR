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
        public static InventoryCell currentCell;
        private List<InventoryCell> cells;
        public RectTransform _container, _draggingParent, viewPort;
        public Text descriptionText, capacityText;
        public Button DeleteItemButton;
        public Image imageInventory;
        public MessageBox messageBoxOnDeleteItem;

        public static InventoryItemRenderer instance;

        private void Awake()
        {
            instance = this;
        }

        private void Start() // подождать Awake у инвентаря плеера
        {
            InitItemCells();
            Core.RTManager.GetPlayerInventory().OnItemsAddCallback += AddInventoryItem;
            Core.RTManager.GetPlayerInventory().OnItemsUpdateCallback += UpdateInventoryItem;
            Core.Inventory.PlayerInventory.OnItemDeleteCallback += DeleteInventoryItem;

            messageBoxOnDeleteItem.Init("Delete this item?", "Yes", "No!");
            messageBoxOnDeleteItem.onYes = () =>
            {
                Core.Inventory.PlayerInventory.instance.DeleteItem(currentCell._playerItem);
                imageInventory.color = new Color(255, 255, 255, 100);
                deleteItemButtonPressed = false;
                messageBoxOnDeleteItem.Close();
            };
            messageBoxOnDeleteItem.onNo = () =>
            {
                currentCell.ReturnOnLastPlace();
                messageBoxOnDeleteItem.Close();
            };

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
                tmp.icon.raycastTarget = false; // если ячейка пустая
                cells.Add(tmp);
            }
            UpdateCapacityText();
        }

        // TODO: append new items instead of overwriting from start
        private void AddInventoryItem(Core.Inventory.PlayerItem newItem)
        {
            currentCell = Instantiate(newCell, _container);
            //inventory.SetIndex(newItem, currentCell.transform.GetSiblingIndex());
            currentCell.Init(newItem, _draggingParent);
            currentCell.Render();

            cells.Add(currentCell);
            UpdateCapacityText();
        }

        private void UpdateInventoryItem(Core.Inventory.PlayerItem playerItem)
        {
            currentCell = cells.Find(inventoryCell => inventoryCell._playerItem.Equals(playerItem));
            currentCell.RenderText();
        }

        private void DeleteInventoryItem(Core.Inventory.PlayerItem playerItem)
        {
            currentCell = cells.Find(inventoryCell => inventoryCell._playerItem.Equals(playerItem));

            SetVoidTarget();
            Destroy(currentCell.gameObject);

            cells.Remove(currentCell);
            UpdateCapacityText();
        }

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData) //клик на итем в инвентаре
        {
            currentCell = cells.Find(cel => RectTransformUtility.RectangleContainsScreenPoint(cel.rectTransform, eventData.position));
            if (currentCell != null)
            {
                descriptionText.text = currentCell._playerItem.ToString();
                target.transform.position = currentCell.transform.position;
                target.transform.SetParent(currentCell.transform);

                if (deleteItemButtonPressed)
                    messageBoxOnDeleteItem.Show();
            }
            else
            {
                descriptionText.text = "";
                SetVoidTarget();
            }
        }

        private void SetVoidTarget()
        {
            target.transform.position = new Vector2(-50, -50);
            target.transform.SetParent(_container.parent);
        }

        private void UpdateCapacityText()
        {
            capacityText.text = cells.Count + "/" + Core.Inventory.PlayerInventory.instance.maxItems;
        }

        public static bool deleteItemButtonPressed;
        public void OnClickDeleteItemButton()
        {
            // если уже выбран предмет, то спрашиваем можно ли удалить
            if (!target.transform.parent.Equals(_container.parent) && !deleteItemButtonPressed)
            {
                currentCell.lastSibIndex = currentCell.transform.GetSiblingIndex();
                messageBoxOnDeleteItem.Show();
                return;
            }

            // предмет не выбран, следующий клик по итему и будет предложено удаление
            if (deleteItemButtonPressed)
            {
                imageInventory.color = new Color(255, 255, 255, 100);
                deleteItemButtonPressed = false;
            }
            else
            {
                imageInventory.color = new Color(255, 0, 0, 100);
                deleteItemButtonPressed = true;
            }
        }
    }
}