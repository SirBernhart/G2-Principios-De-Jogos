﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    [SerializeField] RectTransform preparationTableButton;
    private bool isCountingDoubleClick;
    private GameObject currentHeldGameObject;
    private Vector2 heldGameObjectOriginalPosition;

    // Souns
    [SerializeField] private AudioSource lixo;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentHeldGameObject = GetObjectUnderMouse();

            if (currentHeldGameObject != null)
            {
                heldGameObjectOriginalPosition = currentHeldGameObject.transform.localPosition;

                if (currentHeldGameObject.tag != "Ingrediente" && currentHeldGameObject.tag != "Dish")
                {
                    currentHeldGameObject = null;
                    return;
                }
            }
        }
        if (Input.GetMouseButton(0))
        {
            if(currentHeldGameObject != null && (currentHeldGameObject.tag == "Ingrediente")
                && Vector2.Distance(heldGameObjectOriginalPosition, new Vector2(Input.mousePosition.x, Input.mousePosition.y)) > 10)
            {
                currentHeldGameObject.GetComponent<Collider2D>().enabled = false;

                if (currentHeldGameObject.tag == "Ingrediente")
                {
                    currentHeldGameObject.GetComponent<Ingredient>().isBeingDragged = true;
                }
                
                currentHeldGameObject.transform.position =
                    Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            if(currentHeldGameObject != null && currentHeldGameObject.tag == "Ingrediente" && currentHeldGameObject.GetComponent<Ingredient>().isBeingDragged)
            {
                GameObject cliquedGameObject = GetObjectUnderMouse();
                bool willReturn = true;

                if (cliquedGameObject != null)
                {
                    if (cliquedGameObject.GetComponent<WorkingStation>() != null)
                    {
                        if (cliquedGameObject.GetComponent<WorkingStation>().TryPlaceIngredient(currentHeldGameObject))
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
                        lixo.Play();
                        Destroy(currentHeldGameObject);
                    }
                }
                if (willReturn)
                {
                    currentHeldGameObject.transform.localPosition = 
                        new Vector3(heldGameObjectOriginalPosition.x, heldGameObjectOriginalPosition.y, -1);
                }

                if (currentHeldGameObject.tag == "Ingrediente")
                {
                    currentHeldGameObject.GetComponent<Ingredient>().isBeingDragged = false;
                }

                currentHeldGameObject.GetComponent<Collider2D>().enabled = true;
                currentHeldGameObject = null;
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
        Plate plate = currentHeldGameObject.transform.parent.parent.parent.GetComponent<Plate>();
        if(plate != null)
        {
            if (plate.DeliverPlate())
            {
                return;
            }
        }

    }
}
