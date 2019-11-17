using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{
    private List<Dish> dishesOrdered = new List<Dish>();
    private float waitTimer;
    public float maxWaitTime;

    public Order(List<Dish> possibleDishes)
    {
        MakeAnOrder(possibleDishes);
    }

    public void MakeAnOrder(List<Dish> possibleDishes)
    {
        // aleatoriza quantas dishes serão, assim como quais serão        
    }
}
