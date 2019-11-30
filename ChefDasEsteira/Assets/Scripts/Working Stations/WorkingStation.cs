using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkingStation : MonoBehaviour
{
    [SerializeField] private List<GameObject> acceptableIngredients;
    [SerializeField] private List<GameObject> possibleNewIngredients;

    public bool TryPlaceIngredient(GameObject ingredient)
    {
        if (acceptableIngredients.Count == 0)
        {
            ingredient.transform.SetParent(transform, false);
            ingredient.transform.localPosition = new Vector3(0, 0, -1);
            return true;
        }

        for(int i = 0 ; i < acceptableIngredients.Count ; ++i)
        {
            if(acceptableIngredients[i].name+"(Clone)" == ingredient.name)
            {
                ingredient.transform.SetParent(transform, false);
                ingredient.transform.localPosition = new Vector3(0, 0, -1);
                return true;
            }
        }
        return false;
    }

    public bool MakeNewIngredient()
    {
        Ingredient[] ingredientsInStation = LoadIngredientsInStation();

        // Ingredients that can be made in this working station
        for (int i = 0 ; i < possibleNewIngredients.Count; ++i)
        {
            List<GameObject> requiredIngredients = possibleNewIngredients[i].GetComponent<Ingredient>().requiredIngredients;
            if (requiredIngredients.Count != ingredientsInStation.Length)
            {
                continue;
            }

            int j = 0;
            // Ingredients that are required to make the possible new ingredients
            for (; j < requiredIngredients.Count; ++j)
            {
                int k = 0;
                // Ingredients that have been placed on the working station
                for (; k < ingredientsInStation.Length; ++k)
                {
                    if ((requiredIngredients[j].name + "(Clone)") == ingredientsInStation[k].name)
                    {
                        break;
                    }
                }
                // This new ingredient can't be made with the ingredients in the station
                if(ingredientsInStation.Length == k)
                {
                    break;
                }
            }
            if(j == requiredIngredients.Count)
            {
                ClearIngredientsInStation();
                GameObject newIngredient = Instantiate(possibleNewIngredients[i], transform);
                newIngredient.transform.localPosition = new Vector3(0, 0, -1);
                
                return true;
            }
        }
        
        return false;
    }

    private Ingredient[] LoadIngredientsInStation()
    {
        return transform.GetComponentsInChildren<Ingredient>();
    }

    private void ClearIngredientsInStation()
    {
        for(int i = 0 ; i < transform.childCount ; ++i)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

}
