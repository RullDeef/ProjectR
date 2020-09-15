using System.Collections.Generic;
using UnityEngine;
using Core.Inventory;

namespace Core.Craft
{
    [CreateAssetMenu(fileName = "Recipe", menuName = "ProjectR/Recipe", order = 0)]
    public class Recipe : ScriptableObject
    {
        public List<PlayerItem> ingredients; // необходимые ингредиенты для крафта
        public PlayerItem result; // результат крафта

        // public Recipe(List<PlayerItem> ingredients, PlayerItem result)
        // {
        //     this.ingredients = ingredients;
        //     this.result = result;
        // }

        public bool Craft()
        {
            return PlayerInventory.instance.Craft(ingredients, result);
        }
    }
}

