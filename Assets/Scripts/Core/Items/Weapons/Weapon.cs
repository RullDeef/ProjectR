using UnityEngine;
using System.Collections.Generic;

namespace Core.Items.Weapons
{
    public class Weapon : Item
    {
        public float damage;
        public Weapon(int id, string title, string description, Texture icon, int maxStacks, Type type, Dictionary<string, int> stats, float damage)
            : base(id, title, description, icon, maxStacks, type, stats)
        {
            this.damage = damage;
        }
    }
}