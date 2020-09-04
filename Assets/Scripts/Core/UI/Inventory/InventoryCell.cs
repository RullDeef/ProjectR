using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace UI.Inventory
{
    public class InventoryCell : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public Core.Inventory.Item _item;

        Transform _dragginParent;
        Transform _originalParent;

        public void Init(Transform draggingParent)
        {
            _dragginParent = draggingParent;
            _originalParent = transform.parent;
        }

        public void Render(Core.Inventory.Item item)
        {
            _item = item;
            name = item.name;
            GetComponent<RawImage>().texture = item.slotIcon;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            transform.SetParent(_dragginParent);
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            int closestIndex = 0;
            transform.SetParent(_originalParent);

            for (int i = 0; i < _originalParent.childCount; i++)
            {
                if (Vector2.Distance(transform.position, _originalParent.GetChild(i).position) <
                    Vector2.Distance(transform.position, _originalParent.GetChild(closestIndex).position))
                {
                    closestIndex = i;
                }
            }
            
            transform.SetSiblingIndex(closestIndex);
        }
    }
}
