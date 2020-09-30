using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Core.Inventory;

// TODO: refactor all namespaces. Separate UI from CORE
namespace UI.Inventory
{
    public class InventoryItemRenderer : MonoBehaviour, IPointerClickHandler, IPointerDownHandler
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

        private void Start() // ожидание Awake у инвентаря плеера
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
            newCell._playerItem = new PlayerItem(Core.Craft.PlayerCraft.items["empty"]);

            //cellsCount = Core.RTManager.GetPlayerInventory().maxItems;
            for (int i = 0; i < cellsCount; i++)
            {
                AddInventoryItem(newCell._playerItem);
            }
        }

        private void AddInventoryItem(Core.Inventory.PlayerItem newItem)
        {
            currentCell = Instantiate(newCell, _container);
            currentCell.Init(newItem, _draggingParent);
            currentCell.Render();

            if (newItem.item.id == 0)
                currentCell.icon.raycastTarget = false; // если ячейка пустая

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

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData) // Клик на итем в инвентаре
        {
            currentCell = cells.Find(cel => RectTransformUtility.RectangleContainsScreenPoint(cel.rectTransform, eventData.position));
            if (currentCell != null)
            {
                if (deleteItemButtonPressed)
                {
                    messageBoxOnDeleteItem.Show();
                    return;
                }

                if (eventData.clickCount >= 2)
                    currentCell._playerItem.TryActivateItem();
            }
            else
            {
                SetVoidTarget();
            }
        }

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData) // Начало клика на итем
        {
            currentCell = cells.Find(cel => RectTransformUtility.RectangleContainsScreenPoint(cel.rectTransform, eventData.position));
            if (currentCell != null)
            {
                descriptionText.text = currentCell._playerItem.ToString();
                target.transform.position = currentCell.transform.position;
                target.transform.SetParent(currentCell.transform);
            }
            else
            {
                SetVoidTarget();
            }
        }

        private void SetVoidTarget()
        {
            descriptionText.text = "";
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