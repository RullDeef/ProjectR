using UnityEngine;
using System.Collections.Generic;
using Core.Inventory;

namespace Core.Items.Consumables
{
    public class Consumable : Item
    {
        Dictionary<string, float> effects;
        public Consumable(Item item, Dictionary<string, float> effects)
            : base(item)
        {
            this.effects = effects;
        }

        public override void Use(PlayerItem item)
        {
            // foreach (var effect in effects)
            // {
                
            // }
        }
    }
}