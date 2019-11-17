using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    [SerializeField] private List<Dish> possibleDishes;
    private List<GameObject> currentOrders = new List<GameObject>();

    [SerializeField] private float timeBetweenOrders;
    [SerializeField] private GameObject orderSheet;
    [SerializeField] private Transform orderSheetParent;

    private float timerForNextOrder;
    private void Update()
    {
        timerForNextOrder += Time.deltaTime;
        if(timerForNextOrder >= timeBetweenOrders)
        {
            timerForNextOrder = 0;
            GetNewOrder();
        }
    }

    public void GetNewOrder()
    {
        currentOrders.Add(Instantiate(orderSheet, orderSheetParent));
        currentOrders[currentOrders.Count].GetComponent<Order>().MakeAnOrder(possibleDishes);
    }

}
