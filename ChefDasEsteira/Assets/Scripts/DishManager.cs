using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishManager : MonoBehaviour
{
    public List<GameObject> possibleDishes;
    public List<Transform> ingredientSlots;
    public Transform dishSlot;

    [SerializeField] private GameObject prepareDishButton;

    public bool TryAddIngredientToTable(GameObject ingredient)
    {
        for (int i = 0; i < ingredientSlots.Count; ++i)
        {
            if(ingredientSlots[i].childCount == 0)
            {
                ingredient.transform.SetParent(ingredientSlots[i], false);
                ingredient.transform.localPosition = Vector2.zero;

                return true;
            }
        }

        return false;
    }

    public void PrepareDish()
    {
        List<GameObject> ingredients = new List<GameObject>();
        for(int i = 0 ; i < ingredientSlots.Count ; ++i)
        {
            if (ingredientSlots[i].childCount == 0)
            {
                continue;
            }

            ingredients.Add(ingredientSlots[i].GetChild(0).gameObject);
            Debug.Log(ingredients[i].name);
        }

        GameObject newDish = GetDishMadeByTheseIngredients(ingredients);
        ingredients.Clear();
        // If a dish could be made, instantiates it and clears the ingredients
        if(newDish != null)
        {
            Instantiate(newDish, dishSlot, false).transform.localPosition = Vector2.zero;
            for(int i = 0 ; i < ingredientSlots.Count ; ++i)
            {
                if (ingredientSlots[i].childCount == 0)
                {
                    continue;
                }

                Destroy(ingredientSlots[i].GetChild(0).gameObject);
            }
            return;
        }
        Debug.Log("No dish matches this recipe");
    }

    public GameObject GetDishMadeByTheseIngredients(List<GameObject> ingredients)
    {
        int i = 0;
        for (; i < possibleDishes.Count; ++i)
        {
            if (possibleDishes[i].GetComponent<Dish>().CheckIfIngredientMatchRecipe(ingredients))
            {
                return possibleDishes[i];
            }
        }
        return null;
    }
}
