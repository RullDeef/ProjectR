using UnityEngine;
using Core.Items;

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
                throw new System.ArgumentOutOfRangeException("count > item.maxStacks");
            this.count = count;
        }

        public void TryActivateItem()
        {
            if (item.type == Type.Active)
            {
                //skill?
                count--;
                if (count == 0)
                    PlayerInventory.instance.DeleteItem(this);
                else
                    PlayerInventory.instance.UpdateItem(this);
            }
            else
            {
                Use();
            }
        }

        public override string ToString()
        {
            return item.title + ": " + item.description + " , " + count;
        }

        public void Use()
        {
            item.Use(this);
        }
    }
}