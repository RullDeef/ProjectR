using UnityEngine;
using System.Collections.Generic;

namespace Core.Items
{
    //[CreateAssetMenu(fileName = "Item", menuName = "ProjectR/Item", order = 0)]
    public class Item : ScriptableObject
    {
        public int id;
        public string title;
        public string description;
        public Texture slotIcon;

        public int maxStacks;
        public Type type;
        public Dictionary<string, int> stats;

        public Item(int id, string title, string description, Texture icon, int maxStacks, Type type, Dictionary<string, int> stats)
        {
            this.id = id;
            this.title = title;
            this.description = description;
            slotIcon = icon;
            this.maxStacks = maxStacks;
            this.type = type;
            this.stats = stats;
        }

        public Item(Item item)
        {
            this.id = item.id;
            this.title = item.title;
            this.description = item.description;
            slotIcon = item.slotIcon;
            this.maxStacks = item.maxStacks;
            this.type = item.type;
            this.stats = item.stats;
        }

        public virtual void Use(Core.Inventory.PlayerItem item)
        {

        }
    }

    public enum Type
    {
        Active, Passive, Triggers
    }
}