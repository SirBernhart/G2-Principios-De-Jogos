using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chef : MonoBehaviour
{
    private GameObject currentlyHolding;

    public void HoldObject(GameObject objectToHold)
    {
        if(currentlyHolding == null)
        {
            currentlyHolding = objectToHold;

            // If it is and ingredient
            Ingredient ingScriptRef = objectToHold.GetComponent<Ingredient>();

            //currentlyHolding.transform.SetParent(holdPosition, false);
            currentlyHolding.transform.localPosition = Vector2.zero;
        }
    }

    public void PlaceIngredientOnWorkingStation(GameObject workingStation)
    {
        if (IsHoldingSomething())
        {
            if (currentlyHolding.GetComponent<Ingredient>() != null)
            {
                if (workingStation.GetComponent<WorkingStation>().TryPlaceIngredient(currentlyHolding))
                {
                    currentlyHolding = null;
                }
            }
        }
    }

    public void PlaceDishOnPlate(GameObject plate)
    {
        if (IsHoldingSomething())
        {
            if(currentlyHolding.GetComponent<Dish>() != null)
            {
                if (plate.GetComponent<Plate>().TryToAddDish(currentlyHolding))
                {
                    currentlyHolding = null;
                }
            }
        }
    }

    public void PlaceIngredientOnPreparationTable(GameObject preparationTable)
    {
        if (IsHoldingSomething())
        {
            if(currentlyHolding.GetComponent<Ingredient>() != null)
            {
                bool result = preparationTable.GetComponent<DishManager>().TryAddIngredientToTable(currentlyHolding);
                if (result)
                {
                    currentlyHolding = null;
                }
            }
        }
    }

    public void DiscardObject()
    {
        Destroy(currentlyHolding);
        currentlyHolding = null;
    }

    public void SetNewHeldObjectParent(Transform newParent)
    {
        currentlyHolding.transform.SetParent(newParent, false);
        currentlyHolding.transform.localPosition = Vector2.zero;
        currentlyHolding = null;
    }

    public bool IsHoldingSomething()
    { 
        return currentlyHolding != null;
    }
}
