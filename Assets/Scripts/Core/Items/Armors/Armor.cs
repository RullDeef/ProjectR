using UnityEngine;
using System.Collections.Generic;
using Core.Inventory;

namespace Core.Items.Armors
{
    public abstract class Armor : Item
    {
        public float durabilty;
        public Armor(Item item, float durabilty)
            : base(item)
        {
            this.durabilty = durabilty;
        }
    }
}