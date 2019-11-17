using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{
    private List<Dish> dishesOrdered = new List<Dish>();
    private float waitTimer;
    public float maxWaitTime;

    public void MakeAnOrder(List<Dish> possibleDishes)
    {
        int numberOfDishes = Random.Range(0, 3);

        for(int i = 0 ; i < numberOfDishes ; ++i)
        {
            dishesOrdered.Add(possibleDishes[Random.Range(0, possibleDishes.Count)]);
        }
    }
}
