using UnityEngine;

namespace Core.Inventory
{
    public class PlayerItem
    {
        public Item item;
        public int count;

        public PlayerItem(Item item, int count = 1)
        {
            this.item = item;
            if (count > item.maxStacks)
                throw new System.Exception("count > item.maxStacks");
            this.count = count;
        }

        public override string ToString()
        {
            return item.title + ": " + item.description + " , " + count;
        }
    }
}