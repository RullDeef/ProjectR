using UnityEngine;

namespace Core.Inventory
{
    public class PlayerItem
    {
        public Item item;
        public int count;

        public PlayerItem(Item item)
        {
            this.item = item;
            count = 1;
        }

        public override string ToString()
        {
            return item.title + ": " + item.description + " , " + count;
        }
    }
}