using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace UI.Inventory
{
    public class InventoryCell : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public Core.Inventory.PlayerItem _playerItem;
        public RawImage icon;
        Text currentCountText = null; // Если итем не может быть застакан, это будет null
        public RectTransform rectTransform;

        Transform _dragginParent;
        Transform _originalParent;

        private InventoryCell clone;

        public void Init(Core.Inventory.PlayerItem playerItem, Transform draggingParent)
        {
            _playerItem = playerItem;

            _dragginParent = draggingParent;
            _originalParent = transform.parent;
            icon = GetComponent<RawImage>();
            rectTransform = GetComponent<RectTransform>();

            if (playerItem.item.maxStacks != 1)
                currentCountText = transform.GetChild(0).GetComponent<Text>();
        }

        public void Render()
        {
            name = _playerItem.item.name;
            icon.texture = _playerItem.item.slotIcon;

            if (currentCountText != null)
                currentCountText.text = _playerItem.count.ToString();
        }

        public void RenderText()
        {
            currentCountText.text = _playerItem.count.ToString();
        }

        private int lastSibIndex;
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

            if (RectTransformUtility.RectangleContainsScreenPoint(rectTransform, eventData.position))
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
            if (RectTransformUtility.RectangleContainsScreenPoint(InventoryItemRenderer.instance.
                DeleteItemButton.GetComponent<RectTransform>(), eventData.position))
            {
                Core.Inventory.PlayerInventory.instance.DeleteItem(_playerItem);
            }
        }
    }
}
