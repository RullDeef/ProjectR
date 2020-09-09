using System.Collections.Generic;
using UnityEngine;
using Core.Inventory;

namespace Core.Craft
{
    public class Recipe : MonoBehaviour
    {
        List<PlayerItem> ingredients; // необходимые ингредиенты для крафта
        Item result; // результат крафта

        public void Craft()
        {
            PlayerInventory.instance.Craft(ingredients, result);
        }
    }
}

