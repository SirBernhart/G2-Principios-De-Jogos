using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    [SerializeField] private List<Dish> possibleDishes;
    private List<Order> currentOrders = new List<Order>();
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetNewOrder()
    {
        currentOrders.Add(new Order(possibleDishes));
    }

}
