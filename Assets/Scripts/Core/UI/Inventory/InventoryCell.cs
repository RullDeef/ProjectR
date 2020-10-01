﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace UI.Inventory
{
    public class InventoryCell : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public Core.Inventory.PlayerItem _playerItem;
        public RawImage icon;
        public RectTransform rectTransform;

        private Text currentCountText = null; // Если итем не может быть застакан, это будет null
        private Transform _dragginParent;
        private Transform _originalParent;
        private InventoryCell clone;
        public int lastSibIndex;

        public void Init(Core.Inventory.PlayerItem playerItem, Transform draggingParent)
        {
            _playerItem = playerItem;

            _dragginParent = draggingParent;
            _originalParent = transform.parent;

            icon = GetComponent<RawImage>();
            rectTransform = GetComponent<RectTransform>();
            currentCountText = transform.GetChild(0).GetComponent<Text>();
        }

        public void Render()
        {
            name = _playerItem.item.title;
            icon.texture = _playerItem.item.slotIcon;

            if (_playerItem.item.maxStacks != 1)
                currentCountText.text = _playerItem.count.ToString();
        }

        public void RenderText()
        {
            currentCountText.text = _playerItem.count.ToString();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            lastSibIndex = transform.GetSiblingIndex();
            clone = Instantiate(this, _originalParent);
            clone.transform.SetSiblingIndex(lastSibIndex);
            transform.SetParent(_dragginParent);
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Destroy(clone.gameObject);

            // если итем после переноса остался в поле инвентаря, то его надо поставить в ближайшую клетку
            if (RectTransformUtility.RectangleContainsScreenPoint(InventoryItemRenderer.instance._draggingParent, eventData.position))
            {
                int closestIndex = 0;
                for (int i = 0; i < _originalParent.childCount; i++)
                {
                    if (Vector2.Distance(transform.position, _originalParent.GetChild(i).position) <
                        Vector2.Distance(transform.position, _originalParent.GetChild(closestIndex).position))
                    {
                        closestIndex = i;
                    }
                }

                transform.SetParent(_originalParent);
                if (lastSibIndex < closestIndex) closestIndex++;
                transform.SetSiblingIndex(closestIndex);
            }

            // если итем переместили на кнопку удалить
            if (RectTransformUtility.RectangleContainsScreenPoint(InventoryItemRenderer.instance.
                DeleteItemButton.GetComponent<RectTransform>(), eventData.position))
            {
                InventoryItemRenderer.instance.messageBoxOnDeleteItem.Show();
            }
        }

        // Вернуть итем на прежнее место
        public void ReturnOnLastPlace()
        {
            transform.SetParent(_originalParent);
            transform.SetSiblingIndex(lastSibIndex);
        }
    }
}
