using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos2D = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if(hit.collider != null)
            {
                if (!gameObject.GetComponent<Chef>().IsHoldingIngredient())
                {
                    gameObject.GetComponent<Chef>().HoldIngredient(hit.collider.gameObject);
                }
            }
        }        
    }
}
