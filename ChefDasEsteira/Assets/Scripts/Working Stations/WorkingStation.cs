using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WorkingStation : MonoBehaviour, IDraggableObjectReceiver, IDraggableObjectProvider
{
    [SerializeField] private List<Ingredient> acceptableIngredients;
    [SerializeField] private List<Ingredient> possibleNewIngredients;
    [SerializeField] private float preparationDuration = 2.5f;
    [SerializeField] private Transform ingredientParent;
    
    [Header("Animation")]
    [SerializeField] private AnimationClip preparationAnimation;
    [SerializeField] private KittenAnimationController animationController;
    [SerializeField] private Transform kittenPosition;

    [Header("SFX")]
    [SerializeField] private AudioSource soundToPlay;

    private Stack<Ingredient> ingredientsInStation = new();
    private bool AcceptsAnyIngredient => acceptableIngredients.Count == 0;

    public bool TryPlaceIngredient(Ingredient newIngredient)
    {
        if (AcceptsAnyIngredient
            || acceptableIngredients.Any(ing => ing.Id.Equals(newIngredient.Id)))
        {
            return SetIngredientPositionAndTryMakeRecipe();
        }
        
        return false;

        bool SetIngredientPositionAndTryMakeRecipe()
        {
            ingredientsInStation.Push(newIngredient);
            newIngredient.SetNewParentContainer(ingredientParent);
            TryMakeNewIngredient();
            return true;
        }
    }

    public bool TryMakeNewIngredient()
    {
        // Ingredients that can be made in this working station
        for (int i = 0 ; i < possibleNewIngredients.Count; ++i)
        {
            List<Ingredient> requiredIngredients = possibleNewIngredients[i].requiredIngredients;
            if (requiredIngredients.Count != ingredientsInStation.Count)
            {
                continue;
            }

            // Ingredients that are required to make the possible new ingredients
            foreach (Ingredient requiredIngredient in requiredIngredients)
            {
                Ingredient equalIngredient = ingredientsInStation.FirstOrDefault(ing => ing.Id.Equals(requiredIngredient.Id));
                if (equalIngredient == null)
                {
                    break;
                }

                ClearIngredientsInStation();
                Ingredient newIngredient = Instantiate(possibleNewIngredients[i], ingredientParent);
                newIngredient.transform.localPosition = new Vector3(0, 0, -1);
                
                ingredientsInStation.Push(newIngredient);
                newIngredient.GetComponent<Collider2D>().enabled = false;
                
                PlayAnimation();
                soundToPlay.Play();
                return true;
            }
        }
        
        return false;
    }

    private void ClearIngredientsInStation()
    {
        for(int i = ingredientsInStation.Count ; i > 0 ; --i)
        {
            Destroy(ingredientsInStation.Pop().gameObject);
        }
    }

    private void PlayAnimation()
    {
        if (preparationAnimation == null)
        {
            return;
        }
        
        animationController.PlayAnimation(preparationAnimation, kittenPosition.position, preparationDuration);
    }

    public bool TryReceiveDraggableObject(IDraggableObject obj)
    {
        if (obj is Ingredient ingredient)
        {
            return TryPlaceIngredient(ingredient);
        }

        return false;
    }

    public bool TryGetDraggableObject(out IDraggableObject obj, out Action<bool> onDragFinishedCallback)
    {
        if (ingredientsInStation.TryPeek(out Ingredient ingredient))
        {
            obj = ingredient;
            
            onDragFinishedCallback = OnDragFinishedCallback;
            return true;
        }

        obj = null;
        onDragFinishedCallback = null;
        return false;
    }

    private void OnDragFinishedCallback(bool releasedOnValidPosition)
    {
        if (releasedOnValidPosition)
        {
            ingredientsInStation.Pop();
        }
    }
}