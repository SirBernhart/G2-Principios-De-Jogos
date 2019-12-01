using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DishManager : MonoBehaviour
{
    public List<GameObject> possibleDishes;
    [SerializeField] private Transform slotsParent;
    private List<Transform> ingredientSlots;
    public Transform dishSlot;

    // Sounds
    [SerializeField] private AudioSource madeDish;

    private void Start()
    {
        ingredientSlots = new List<Transform>();
        for(int i = 0 ; i < slotsParent.childCount ; ++i)
        {
            ingredientSlots.Add(slotsParent.GetChild(i));
        }
    }

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

    public bool TryPrepareDish()
    {
        List<GameObject> ingredientsToPrepare = new List<GameObject>();
        for(int i = 0 ; i < ingredientSlots.Count ; ++i)
        {
            if (ingredientSlots[i].childCount == 0)
            {
                continue;
            }

            ingredientsToPrepare.Add(ingredientSlots[i].GetChild(0).gameObject);
        }

        GameObject newDish = GetDishMadeByTheseIngredients(ingredientsToPrepare);
        ingredientsToPrepare.Clear();
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
            madeDish.Play();
            return true;
        }
        Debug.Log("No dish matches this recipe");
        return false;
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
