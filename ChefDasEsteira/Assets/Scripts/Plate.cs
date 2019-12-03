using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour
{
    public List<GameObject> dishesInPlate = new List<GameObject>();
    [SerializeField] private OrderManager orderManagerRef;
    [SerializeField] private Transform dishSpotsParent;
    [SerializeField] private GameObject deliverButtonRef;
    private List<Transform> dishSpots = new List<Transform>();

    private void Start()
    {
        for(int i = 0 ; i < 1 ; ++i)
        {
            dishSpots.Add(dishSpotsParent.GetChild(i));
        }
    }

    public bool TryToAddDish(GameObject dish)
    {
        if(dishesInPlate.Count >= 4)
        {
            return false;
        }
        dishesInPlate.Add(dish);

        for(int i = 0 ; i < dishSpots.Count ; ++i)
        {
            if(dishSpots[i].childCount == 0)
            {
                dish.transform.SetParent(dishSpots[i], false);
                dish.transform.localPosition = new Vector3(0, 0, -1);
            }
        }
        DeliverPlate();

        return true;
    }

    public bool DeliverPlate()
    {
        GameObject completedOrder = orderManagerRef.CheckIfCanCompleteOrder(dishesInPlate);
        if(completedOrder != null)
        {
            ClearDishSpots();
            orderManagerRef.CompleteOrder(completedOrder);
            return true;
        }
        else
        {
            orderManagerRef.FailOrder(orderManagerRef.currentOrders[0]);
            ClearDishSpots();
        }
        return false;
    }

    public void ClearDishSpots()
    {
        for(int i = 0 ; i < dishesInPlate.Count ; i++)
        {
            Destroy(dishesInPlate[i]);
        }
        dishesInPlate.Clear();
    }
}
