using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Core.Craft;

namespace UI.Craft
{
    public class CraftRenderer : MonoBehaviour, IPointerDownHandler
    {
        private List<CraftCell> cells;
        private int cellsCount;
        public RectTransform _container;

        public GameObject emptyRepice;
        private CraftCell newRecipe;

        public static CraftCell currentCell;
        //public MessageBox messageBoxOnCraftItem;

        private void Start() // Ожидание Awake у PlayerCraft
        {
            InitRecipesCells();

            gameObject.SetActive(false); // временный фикс
        }

        private void InitRecipesCells()
        {
            cells = new List<CraftCell>();
            newRecipe = emptyRepice.GetComponent<CraftCell>();

            foreach (Recipe recipe in PlayerCraft.recipes)
            {
                AddCraftRecipe(recipe);
            }
        }

        public void AddCraftRecipe(Recipe recipe)
        {
            currentCell = Instantiate(newRecipe, _container);
            currentCell.Init(recipe);

            cells.Add(currentCell);
        }

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData) //клик на рецепт в окне крафта
        {
            currentCell = cells.Find(cel => RectTransformUtility.RectangleContainsScreenPoint(cel.rectTransform, eventData.position));
            if (currentCell != null)
            {
                // баг только после того как скрафтишь по 1, 2, 1! рецпету, на последнем баг
                if (currentCell._recipe.Craft()) // БАГ здесь, в _recipe неверный result, но верные ингредиенты
                {
                    Debug.Log(currentCell.transform.GetSiblingIndex() + " " +
                        currentCell._recipe.result.item.title + " is craft! " + currentCell._recipe.result.count);
                }
            }
            else
            {

            }
        }
    }
}
