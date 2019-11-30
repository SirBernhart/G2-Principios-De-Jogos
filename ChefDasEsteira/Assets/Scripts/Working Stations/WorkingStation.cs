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
        Debug.Log(possibleNewIngredients.Count);
        for(int i = 0 ; i < possibleNewIngredients.Count ; ++i)
        {
            if(possibleNewIngredients[i].GetComponent<Ingredient>().requiredIngredients[0].name + "(Clone)" == transform.GetChild(0).name)
            {
                GameObject newIngredient = Instantiate(possibleNewIngredients[i], transform);
                newIngredient.transform.localPosition = new Vector3(0, 0, -1);
                Destroy(transform.GetChild(0).gameObject);
                return true;
            }
        }
        return false;
    }

}
