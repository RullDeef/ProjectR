using UnityEngine;

namespace Core
{
    public class RTManager : MonoBehaviour
    {
        private static RTManager instance;

        public Inventory.PlayerInventory inventory;

        private void Awake()
        {
            instance = this;
        }

        public static Inventory.PlayerInventory GetPlayerInventory()
        {
            return instance.inventory;
        }
    }
}