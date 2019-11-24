using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dish : MonoBehaviour
{
    [SerializeField] private List<GameObject> requiredIngredients;
    public int pointsReward;

    public bool CheckIfIngredientMatchRecipe(List<GameObject> ingredients)
    {
        if (ingredients.Count != requiredIngredients.Count)
        {
            return false;
        }

        List<int> correctIngredientIndexes = new List<int>();
        for (int i = 0 ; i < requiredIngredients.Count ; i++)
        {
            int j = 0;
            for (; j < ingredients.Count; ++j)
            {
                if (correctIngredientIndexes.Contains(j))
                {
                    continue;
                }
                if ((requiredIngredients[i].name+"(Clone)") == ingredients[j].name)
                {
                    correctIngredientIndexes.Add(j);
                    break;
                }
            }
            if (j == ingredients.Count)
            {
                return false;
            }
            if(correctIngredientIndexes.Count == requiredIngredients.Count)
            {
                return true;
            }
        }
        return false;
    }
}
