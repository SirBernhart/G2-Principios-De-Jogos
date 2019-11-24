using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    [SerializeField] private GameObject dishManager;
    [SerializeField] private GameManager gm;
    private List<GameObject> currentOrders = new List<GameObject>();

    [SerializeField] private float timeBetweenOrders;
    [SerializeField] private GameObject orderSheet;
    [SerializeField] private Transform orderSheetParent;

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
        currentOrders.Add(Instantiate(orderSheet, orderSheetParent));
        currentOrders[currentOrders.Count - 1].GetComponent<Order>().MakeAnOrder(dishManager.GetComponent<DishManager>().possibleDishes);
    }

    public GameObject CheckIfCanCompleteOrder(List<GameObject> dishesInPlate)
    {
        for(int i = 0 ; i < currentOrders.Count ; ++i)
        {
            GameObject correctOrder = currentOrders[i].GetComponent<Order>().CheckIfDishesCompleteOrder(dishesInPlate);
            if (correctOrder != null)
            {
                return correctOrder;
            }
        }
        return null;
    }

    public void FailOrder(GameObject failedOrder)
    {
        gm.IncreaseErrorCount(1);
        currentOrders.Remove(failedOrder);
        Destroy(failedOrder);
    }

    public void CompleteOrder(GameObject completedOrder)
    {
        Debug.Log("Completou!");
        currentOrders.Remove(completedOrder);
        Destroy(completedOrder);
    }

    //public bool 
}
