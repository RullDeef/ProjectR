using UnityEngine;

namespace Core.Inventory
{
    [CreateAssetMenu(fileName = "Item", menuName = "ProjectR/Item", order = 0)]
    public class Item : ScriptableObject
    {
        public new string name;
        public Texture slotIcon;
    }
}