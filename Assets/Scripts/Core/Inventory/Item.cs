using UnityEngine;

namespace Core.Inventory
{
    [CreateAssetMenu(fileName = "Item", menuName = "ProjectR/Item", order = 0)]
    public class Item : ScriptableObject
    {
        public int id;
        public string title;
        public string description;
        public Texture slotIcon;

        public int maxStacks;
    }
}