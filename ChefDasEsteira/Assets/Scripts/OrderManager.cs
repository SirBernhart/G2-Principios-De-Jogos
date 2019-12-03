using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> possibleOrders;
    [SerializeField] private GameManager gm;
    [SerializeField] private ScoreManager sm;
    [HideInInspector]public List<GameObject> currentOrders = new List<GameObject>();

    [SerializeField] private float timeBetweenOrders;
    [SerializeField] private GameObject orderSheet;
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
        currentOrders.Add(Instantiate(orderSheet, orderSheetParent));
        currentOrders[currentOrders.Count - 1].GetComponent<Order>().MakeAnOrder(possibleOrders);
        newOrder.Play();
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
        if(currentOrders.Count > 0)
        {
            gm.IncreaseErrorCount(1);
            Instantiate(redX, errorCounter);
            currentOrders.Remove(failedOrder);
            Destroy(failedOrder);
            orderFailed.Play();
        }
    }

    public void CompleteOrder(GameObject completedOrder)
    {
        sm.IncreaseScore(completedOrder.GetComponent<Order>().totalScore);
        currentOrders.Remove(completedOrder);
        Destroy(completedOrder);
        orderComplete.Play();
    }
}
