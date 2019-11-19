using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    [SerializeField] RectTransform preparationTableButton;
    // Update is called once per frame
    void Update()
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(preparationTableButton, Input.mousePosition))
        { 
            //ignore the click 
        }
        else if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos2D = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if(hit.collider != null)
            {
                Chef chefRef = gameObject.GetComponent<Chef>();
                // If the object is an Ingredient
                if (hit.collider.GetComponent<Ingredient>() != null)
                {
                    chefRef.HoldObject(hit.collider.gameObject);
                }
                else if (hit.collider.GetComponent<DishManager>() != null)
                {
                    chefRef.PlaceIngredientOnPreparationTable(hit.collider.gameObject);
                }
                else if (hit.collider.GetComponent<WorkingStation>())
                {
                    chefRef.PlaceIngredientOnWorkingStation(hit.collider.gameObject);
                }
                else if(hit.collider.GetComponent<Dish>() != null)
                {
                    chefRef.HoldObject(hit.collider.gameObject);
                }
                else if (hit.collider.GetComponent<Plate>() != null)
                {
                    chefRef.PlaceDishOnPlate(hit.collider.gameObject);
                }
                else if(hit.collider.tag == "Lixeira")
                {
                    chefRef.DiscardObject();
                }
            }
        }        
    }
}
