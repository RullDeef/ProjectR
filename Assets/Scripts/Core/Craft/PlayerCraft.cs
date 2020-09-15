using System;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Core.Inventory;

namespace Core.Craft
{
    public class PlayerCraft : MonoBehaviour
    {
        public TextAsset fileWithRecipes;

        public static Dictionary<string, Item> items;
        public static List<Recipe> recipes;
        public List<PlayerItem> ingredients;
        string[] strItems; // Item(s) in Assets/Items
        string path, nameOfItem;
        void Awake()
        {
            items = new Dictionary<string, Item>();
            strItems = AssetDatabase.FindAssets("t:Item");
            foreach (string guid in strItems)
            {
                path = AssetDatabase.GUIDToAssetPath(guid);
                nameOfItem = Path.GetFileName(path).Split('.')[0]; // name.asset -> name
                items.Add(nameOfItem, AssetDatabase.LoadAssetAtPath<Item>(path));
            }

            recipes = new List<Recipe>();

            string[] sRecipes = fileWithRecipes.text.Split('\n');
            string[] tmp, strIngredients;
            string strResult, strItem;
            int count;

            // try
            // {
                // Из каждой строки в файле получить рецепт
                foreach (string str in sRecipes)
                {
                    tmp = str.Split('='); // делим строку на строку ингредиентов и строку результата

                    strIngredients = tmp[0].Split(','); // строка ингредиентов
                    strResult = tmp[1]; // строка результата

                    // Получение результата
                    tmp = strResult.Split('|');
                    strItem = tmp[0];
                    count = int.Parse(tmp[1]);
                    PlayerItem result = new PlayerItem(items[strItem], count);

                    // Получение ингредиентов
                    ingredients = new List<PlayerItem>();
                    foreach (var sItem in strIngredients)
                    {
                        tmp = sItem.Split('|');
                        strItem = tmp[0];
                        count = int.Parse(tmp[1]);
                        AddItem(new PlayerItem(items[strItem], count));
                    }

                    // Добавление рецепта в список рецептов
                    Recipe recipe = ScriptableObject.CreateInstance<Recipe>();
                    recipe.ingredients = ingredients;
                    recipe.result = result;

                    recipes.Add(recipe);
                }
            // }
            // catch (FormatException)
            // {
            //     Debug.LogError("Repieces.txt имеет неправильный формат!");
            // }
        }


        public void AddItem(PlayerItem itemToAdd)
        {
            PlayerItem existingPlayerItem = ingredients.Find(_ => _.item.id == itemToAdd.item.id && _.count < _.item.maxStacks);
            if (existingPlayerItem != null)
            {
                int availableSpace = itemToAdd.item.maxStacks - existingPlayerItem.count;
                if (availableSpace >= itemToAdd.count)
                {
                    existingPlayerItem.count += itemToAdd.count;
                }
                else // Если места в существующем итеме не хватит
                {
                    existingPlayerItem.count += availableSpace;
                    PlayerItem newItem = new PlayerItem(itemToAdd.item, itemToAdd.count - availableSpace);
                    ingredients.Add(newItem);
                }
            }
            else
            {
                ingredients.Add(itemToAdd);
            }
        }
    }
}
