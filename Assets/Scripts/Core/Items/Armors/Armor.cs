using UnityEngine;
using System.Collections.Generic;

namespace Core.Items.Armors
{
    public class Armor : Item
    {
        public float durabilty;
        public Armor(int id, string title, string description, Texture icon, int maxStacks, Type type, Dictionary<string, int> stats, float durabilty)
            : base(id, title, description, icon, maxStacks, type, stats)
        {
            this.durabilty = durabilty;
        }
    }
}