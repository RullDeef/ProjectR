using UnityEngine;
using System.Collections.Generic;

namespace Core.Items
{
    [CreateAssetMenu(fileName = "Item", menuName = "ProjectR/Item", order = 0)]
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

        public virtual bool Use()
        {
            return true;
        }
    }

    public enum Type
    {
        Active, Passive, Triggers
    }
}