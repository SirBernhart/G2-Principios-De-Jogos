using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    [HideInInspector] public bool isBeingDragged;
    public List<GameObject> requiredIngredients;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "fim esteira")
        {
            Destroy(this.gameObject);
        }
    }
}
