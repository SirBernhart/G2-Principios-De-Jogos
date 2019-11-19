using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorkingStation : MonoBehaviour
{
    [SerializeField] private List<GameObject> acceptableIngredients;
    [SerializeField] private List<GameObject> possibleNewIngredients;
    [SerializeField] private Image stationImage;
    [SerializeField] private Chef chefRef;

    private void OnMouseEnter()
    {
        // Indica se o ingrediente selecionado é tratavel nessa estação ou não
    }

    public bool TryPlaceIngredient(GameObject ingredient)
    {
        for(int i = 0 ; i < acceptableIngredients.Count ; ++i)
        {
            if(acceptableIngredients[i].name+"(Clone)" == ingredient.name)
            {
                ingredient.transform.SetParent(transform, false);
                return true;
            }
        }
        return false;
    }

    /*private void OnMouseUp()
    {
        switch (chefRef.GetIngredientName())
        {
            case Ingredient.names.Salmao:

                MakeNewIngredient(Ingredient.names.SalmaoFatiado);
                break;
        }
    }*/

    public void MakeNewIngredient()
    {
        for(int i = 0 ; i < possibleNewIngredients.Count ; ++i)
        {
            if(possibleNewIngredients[i].GetComponent<Ingredient>().requiredIngredients[0].name + "(Clone)" == transform.GetChild(1).name)
            {
                Instantiate(possibleNewIngredients[i], transform).GetComponent<Ingredient>().isInEsteira = false ;
                Destroy(transform.GetChild(1).gameObject);
            }
        }
    }

}
