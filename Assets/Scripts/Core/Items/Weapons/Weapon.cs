using UnityEngine;
using System.Collections.Generic;
using Core.Inventory;

namespace Core.Items.Weapons
{
    //[CreateAssetMenu(fileName = "Weapon", menuName = "ProjectR/!GAME/Items/Weapons", order = 0)]
    public class Weapon : Item
    {
        public float damage;
        public Weapon(Item item, float damage)
            : base(item)
        {
            this.damage = damage;
        }

        public override void Use(Core.Inventory.PlayerItem item)
        {
            PlayerPanel.instance.PutOn(PartOfBody.RightHand, item);
        }
    }
}