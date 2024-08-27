using System;
using System.Collections.Generic;
using UnityEngine;

public class Plate : MonoBehaviour, IDraggableObjectReceiver
{
    [SerializeField] private OrderManager orderManagerRef;
    [SerializeField] private Transform dishSpotsParent;
    [SerializeField] private GameObject deliverButtonRef;
    private List<Transform> dishSpots = new ();
    
    private void Start()
    {
        for(int i = 0 ; i < 1 ; ++i)
        {
            dishSpots.Add(dishSpotsParent.GetChild(i));
        }
    }

    private void DeliverPlate(Ingredient ingredient)
    {
        orderManagerRef.TryCompleteOrder(ingredient);
        Destroy(ingredient.gameObject);
    }

    public bool TryReceiveDraggableObject(IDraggableObject obj)
    {
        DeliverPlate(obj as Ingredient);
        return true;
    }
}