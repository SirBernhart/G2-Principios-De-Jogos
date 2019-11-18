using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlicingTable : MonoBehaviour
{
    [SerializeField] private List<GameObject> acceptableIngredients;
    [SerializeField] private List<GameObject> possibleNewIngredients;
    [SerializeField] private Image stationImage;
    [SerializeField] private Chef chefRef;

    private void OnMouseEnter()
    {
        // Indica se o ingrediente selecionado é tratavel nessa estação ou não
    }

    private void OnMouseUp()
    {
        switch (chefRef.GetIngredientName())
        {
            case Ingredient.names.Salmao:
                chefRef.ReleaseIngredient();

                MakeNewIngredient(Ingredient.names.SalmaoFatiado);
                break;
        }
    }

    private void MakeNewIngredient(Ingredient.names ingName)
    {
        for(int i = 0 ; i < possibleNewIngredients.Count ; ++i)
        {
            if(possibleNewIngredients[i].GetComponent<Ingredient>().ingredientName == ingName)
            {
                Instantiate(possibleNewIngredients[i], transform);
            }
        }
    }

}
