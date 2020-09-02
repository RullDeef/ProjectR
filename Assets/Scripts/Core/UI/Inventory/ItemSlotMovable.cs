using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace UI.Inventory
{
    public class ItemSlotMovable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        static Transform parentTransform;

        void Awake()
        {
            parentTransform = GetComponentInParent<Transform>();
            transform.SetParent(parentTransform);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {

        }

        public void OnDrag(PointerEventData eventData)
        {
            //Debug.Log ("OnDrag");

            //this.transform.position = eventData.position;
            transform.position += (Vector3)eventData.delta;

        }

        public void OnEndDrag(PointerEventData eventData)
        {
            transform.position = new Vector2((int)(transform.position.x - transform.position.x % 16), (int)(transform.position.y + transform.position.y % 16));
            Debug.Log("OnEndDrag");

        }
    }
}
