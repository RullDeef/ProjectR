using UnityEngine;
using UnityEngine.UI;
using Core.Craft;

namespace UI.Craft
{
    public class CraftCell : MonoBehaviour
    {
        public Recipe _recipe;

        public RectTransform rectTransform, _containerForIngredients, _rectResult;
        public GameObject itemSlot;
        private GameObject tmp;

        public void Init(Recipe recipe)
        {
            _recipe = recipe;

            rectTransform = GetComponent<RectTransform>();

            foreach (var ingredient in recipe.ingredients)
            {
                tmp = Instantiate(itemSlot, _containerForIngredients);
                tmp.GetComponent<RawImage>().texture = ingredient.item.slotIcon;
                if (ingredient.item.maxStacks != 1)
                    tmp.GetComponentInChildren<Text>().text = ingredient.count.ToString();
            }
            tmp = Instantiate(itemSlot, _rectResult);
            tmp.GetComponent<RawImage>().texture = _recipe.result.item.slotIcon;
            if (_recipe.result.item.maxStacks != 1)
                tmp.GetComponentInChildren<Text>().text = _recipe.result.count.ToString();
        }
    }
}

