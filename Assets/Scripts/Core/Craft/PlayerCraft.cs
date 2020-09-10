using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Inventory;

namespace Core.Craft
{
    public class PlayerCraft : MonoBehaviour
    {
        public TextAsset fileWithRecipes;
        List<Recipe> recipes;
        void Awake()
        {
            recipes = new List<Recipe>();

            string[] sRecipes = fileWithRecipes.text.Split('\n');
            string[] tmp, strIngredients;
            string strResult, strItem;
            int count;
            foreach (string str in sRecipes)
            {
                tmp = str.Split('=');

                strIngredients = tmp[0].Split(',');
                strResult = tmp[1];

                tmp = strResult.Split('|');
                strItem = tmp[0];
                count = int.Parse(tmp[1]);
                PlayerItem result = new PlayerItem(null, count);

                strIngredients = tmp[0].Split(',');
            }
        }
    }
}

