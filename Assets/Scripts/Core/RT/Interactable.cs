using UnityEngine;

namespace Core.RT
{
    public class Interactable : MonoBehaviour
    {
        public float interactRadius = 1.0f;

        public virtual void Interact()
        {
            // override in derived
        }

        protected void Awake()
        {
            SphereCollider collider = gameObject.AddComponent<SphereCollider>();
            collider.radius = interactRadius;
            collider.isTrigger = true;
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, interactRadius);
        }

        public bool CanInteract()
        {
            float distance = Vector3.Distance(transform.position, GameManager.GetPlayer().position);
            return distance <= interactRadius;
        }
    }
}