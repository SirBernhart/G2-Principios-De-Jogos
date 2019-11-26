using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    [SerializeField] RectTransform preparationTableButton;
    private bool isCountingDoubleClick;
    [SerializeField] private float maxDoubleClickTime;

    private float doubleClickTimer;
    void Update()
    {
        if (isCountingDoubleClick)
        {
            doubleClickTimer += Time.deltaTime;
            if(doubleClickTimer >= maxDoubleClickTime)
            {
                isCountingDoubleClick = false;
                doubleClickTimer = 0;
            }
        }

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
                    if (!isCountingDoubleClick)
                    {
                        isCountingDoubleClick = true;
                    }
                    else
                    {
                        Debug.Log("Double click");
                        //Do double click
                    }
                    //chefRef.HoldObject(hit.collider.gameObject);
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
