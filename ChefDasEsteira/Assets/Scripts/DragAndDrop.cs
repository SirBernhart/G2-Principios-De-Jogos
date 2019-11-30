using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    [SerializeField] RectTransform preparationTableButton;
    private bool isCountingDoubleClick;
    private GameObject currentHeldGameObject;
    private Vector2 heldGameObjectOriginalPosition;

    // Double click variables
    private bool doubleClicked;
    private float doubleClickTimer;
    [SerializeField] private float maxDoubleClickTime;

    void Update()
    {
        doubleClicked = false;
        if (isCountingDoubleClick)
        {
            if (doubleClickTimer >= maxDoubleClickTime)
            {
                isCountingDoubleClick = false;
                doubleClickTimer = 0;
            }
            doubleClickTimer += Time.deltaTime;
        }

        if (Input.GetMouseButtonDown(0))
        {
            currentHeldGameObject = GetObjectUnderMouse();

            if (currentHeldGameObject != null)
            {
                heldGameObjectOriginalPosition = currentHeldGameObject.transform.localPosition;

                if (currentHeldGameObject.tag == "Ingrediente" || currentHeldGameObject.tag == "Dish")
                {
                    if (!isCountingDoubleClick)
                    {
                        isCountingDoubleClick = true;
                    }
                    else
                    {
                        ExecuteStationAction();
                        isCountingDoubleClick = false;
                        doubleClicked = true;
                    }
                }
                else
                {
                    currentHeldGameObject = null;
                    return;
                }
            }
        }
        if (Input.GetMouseButton(0))
        {
            if(currentHeldGameObject != null && (currentHeldGameObject.tag == "Ingrediente" || currentHeldGameObject.tag == "Dish")
                && Vector2.Distance(heldGameObjectOriginalPosition, new Vector2(Input.mousePosition.x, Input.mousePosition.y)) > 10)
            {
                currentHeldGameObject.GetComponent<Collider2D>().enabled = false;

                if (currentHeldGameObject.tag == "Ingrediente")
                {
                    currentHeldGameObject.GetComponent<Ingredient>().isBeingDragged = true;
                }
                else if(currentHeldGameObject.tag == "Dish")
                {
                    currentHeldGameObject.GetComponent<Dish>().isBeingDragged = true;
                }
                
                currentHeldGameObject.transform.position =
                    Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
            }
        }
        if (!doubleClicked && Input.GetMouseButtonUp(0))
        {
            if (currentHeldGameObject != null)
            {
                bool canContinue = false;
                if(currentHeldGameObject.tag == "Ingrediente" && currentHeldGameObject.GetComponent<Ingredient>().isBeingDragged)
                {
                    canContinue = true;
                }
                else if(currentHeldGameObject.tag == "Dish" && currentHeldGameObject.GetComponent<Dish>().isBeingDragged)
                {
                    canContinue = true;
                }

                if (canContinue)
                {
                    GameObject cliquedGameObject = GetObjectUnderMouse();
                    bool willReturn = true;

                    if (!isCountingDoubleClick && cliquedGameObject != null)
                    {
                        if (cliquedGameObject.GetComponent<WorkingStation>() != null)
                        {
                            if (cliquedGameObject.GetComponent<WorkingStation>().TryPlaceIngredient(currentHeldGameObject))
                            {
                                willReturn = false;
                            }
                        }
                        else if (cliquedGameObject.GetComponent<DishManager>() != null)
                        {
                            if (cliquedGameObject.GetComponent<DishManager>().TryAddIngredientToTable(currentHeldGameObject))
                            {
                                willReturn = false;
                            }
                        }
                        else if (cliquedGameObject.GetComponent<Plate>() != null)
                        {
                            if (cliquedGameObject.GetComponent<Plate>().TryToAddDish(currentHeldGameObject))
                            {
                                willReturn = false;
                            }
                        }
                        else if (cliquedGameObject.tag == "Lixeira")
                        {
                            Destroy(currentHeldGameObject);
                        }
                    }
                    if (willReturn)
                    {
                        currentHeldGameObject.transform.localPosition = heldGameObjectOriginalPosition;
                    }

                    if (currentHeldGameObject.tag == "Ingrediente")
                    {
                        currentHeldGameObject.GetComponent<Ingredient>().isBeingDragged = false;
                    }
                    else if (currentHeldGameObject.tag == "Dish")
                    {
                        currentHeldGameObject.GetComponent<Dish>().isBeingDragged = false;
                    }

                    currentHeldGameObject.GetComponent<Collider2D>().enabled = true;
                    currentHeldGameObject = null;
                }
            }
        }
    }
    private GameObject GetObjectUnderMouse()
    {
        Vector2 mousePos2D = 
            Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector3.zero);
        if (hit)
        {
            return hit.collider.gameObject;
        }
        else
        {
            return null;
        }
    }

    private void ExecuteStationAction()
    {
        WorkingStation ws = currentHeldGameObject.transform.parent.GetComponent<WorkingStation>();
        if(ws != null)
        {
            if (ws.MakeNewIngredient())
            {
                return;
            }
        }
        DishManager dm = currentHeldGameObject.transform.parent.parent.parent.GetComponent<DishManager>();
        if(dm != null)
        {
            if (dm.TryPrepareDish())
            {
                return;
            }
        }

    }
}
