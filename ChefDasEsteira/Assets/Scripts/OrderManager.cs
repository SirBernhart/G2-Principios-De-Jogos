using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    [SerializeField] private List<Ingredient> possibleOrders;
    [SerializeField] private GameManager gm;
    [SerializeField] private ScoreManager sm;
    [HideInInspector]public List<Order> currentOrders = new ();

    [SerializeField] private float timeBetweenOrders;
    [SerializeField] private Order orderSheet;
    [SerializeField] private Transform orderSheetParent;
    [SerializeField] private Transform errorCounter;
    [SerializeField] private GameObject redX;

    // Sounds
    [SerializeField] private AudioSource newOrder;
    [SerializeField] private AudioSource orderComplete;
    [SerializeField] private AudioSource orderFailed;

    private void Start()
    {
        timerForNextOrder = timeBetweenOrders - 5;
    }

    private float timerForNextOrder;
    private void Update()
    {
        if(currentOrders.Count <= 4)
        {
            timerForNextOrder += Time.deltaTime;
            if(timerForNextOrder >= timeBetweenOrders)
            {
                timerForNextOrder = 0;
                GetNewOrder();
            }
        }
    }

    public void GetNewOrder()
    {
        Order order = Instantiate(orderSheet, orderSheetParent);
        order.MakeAnOrder(possibleOrders);
        currentOrders.Add(order);
        
        newOrder.Play();
    }

    public Order CheckIfCanCompleteOrder(List<Ingredient> dishesInPlate)
    {
        for(int i = 0 ; i < currentOrders.Count ; ++i)
        {
            Order correctOrder = currentOrders[i].GetComponent<Order>().CheckIfDishesCompleteOrder(dishesInPlate);
            if (correctOrder != null)
            {
                return correctOrder;
            }
        }
        return null;
    }

    public void FailOrder(Order failedOrder)
    {
        if(currentOrders.Count > 0)
        {
            gm.IncreaseErrorCount(1);
            Instantiate(redX, errorCounter);
            currentOrders.Remove(failedOrder);
            Destroy(failedOrder);
            orderFailed.Play();
        }
    }

    public void CompleteOrder(Order completedOrder)
    {
        sm.IncreaseScore(completedOrder.GetComponent<Order>().totalScore);
        currentOrders.Remove(completedOrder);
        Destroy(completedOrder);
        orderComplete.Play();
    }
}