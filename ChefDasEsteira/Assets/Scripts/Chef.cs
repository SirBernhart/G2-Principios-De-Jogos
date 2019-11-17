using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chef : MonoBehaviour
{
    private GameObject currentIngredient;
    [SerializeField] private Transform holdPosition;

    public void HoldIngredient(GameObject ingredient)
    {
        if(currentIngredient == null)
        {
            currentIngredient = ingredient;
            if (ingredient.GetComponent<Ingredient>().isInEsteira)
            {
                ingredient.GetComponent<Ingredient>().RemoveFromEsteira();
            }
            ingredient.transform.SetParent(holdPosition, false);
            ingredient.transform.localPosition = Vector2.zero;
        }
    }

    public void ReleaseIngredient()
    {
        currentIngredient = null;
    }

    public Ingredient.names GetIngredientName()
    {
        return currentIngredient.GetComponent<Ingredient>().ingredientName;
    }

    public bool IsHoldingIngredient()
    { 
        return currentIngredient != null;
    }
}
