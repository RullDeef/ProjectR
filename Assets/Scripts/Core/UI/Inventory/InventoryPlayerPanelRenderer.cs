using UnityEngine;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Core.Inventory;
using System;

namespace UI.Inventory
{
    public class InventoryPlayerPanelRenderer : MonoBehaviour, IPointerClickHandler
    {
        public Text descriptionText;
        public GameObject target;
        public GameObject[] objSlots;
        private List<PlayerPanelCell> slots;
        public RectTransform _container, _draggingParent;
        private PlayerPanelCell currentCell;

        public RawImage defaultIcon;
        public static InventoryPlayerPanelRenderer instance;

        void Start() // ожидание Awake у панели плеера
        {
            instance = this;
            slots = new List<PlayerPanelCell>();
            Core.Inventory.PlayerPanel.instance.OnPutOnItemCallback += RenderPutOn;
            Core.Inventory.PlayerPanel.instance.OnTakeOffItemCallback += RenderTakeOff;
            foreach (GameObject slot in objSlots)
            {
                slots.Add(slot.GetComponent<PlayerPanelCell>());
            }
            transform.parent.gameObject.SetActive(false); // временный фикс (закроет инвентарь)
        }

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData) // Клик на итем в панели игрока
        {
            currentCell = slots.Find(cel => RectTransformUtility.RectangleContainsScreenPoint(cel.rectTransform, eventData.position));
            if (currentCell != null && currentCell._playerItem != null)
            {
                descriptionText.text = currentCell._playerItem.ToString();
                target.transform.position = currentCell.transform.position;
                target.transform.SetParent(currentCell.transform);

                if (eventData.clickCount >= 2)
                    currentCell._playerItem.Use();
            }
            else
            {
                SetVoidTarget();
            }
        }

        void RenderPutOn(PartOfBody part, PlayerItem item)
        {
            slots[(int)part].Init(item);
        }

        void RenderTakeOff(PartOfBody part)
        {
            slots[(int)part].TakeOff();
            SetVoidTarget();
        }

        private void SetVoidTarget()
        {
            descriptionText.text = "";
            target.transform.position = new Vector2(-80, -5);
            target.transform.SetParent(_container.parent);
        }
    }
}