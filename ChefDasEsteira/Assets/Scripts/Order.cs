using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{
    private List<GameObject> dishesOrdered = new List<GameObject>();
    [SerializeField] private GameObject orderPopup;
    private float waitTimer;
    public float maxWaitTime;

    public void MakeAnOrder(List<GameObject> possibleDishes)
    {
        int numberOfDishes = 1;//Random.Range(1, 5);

        for(int i = 0 ; i < numberOfDishes ; ++i)
        {
            dishesOrdered.Add(possibleDishes[Random.Range(0, possibleDishes.Count)]);
            Debug.Log(dishesOrdered[i].name);
        }
        Debug.Log("------------------");
    }

    public void AssembleOrderPopup()
    {
        //orderPopup.
    }

    public GameObject CheckIfDishesCompleteOrder(List<GameObject> dishes)
    {
        if(dishes.Count != dishesOrdered.Count)
        {
            return null;
        }

        List<int> correctDishesIndexes = new List<int>();
        for(int i = 0 ; i < dishesOrdered.Count ; ++i)
        {
            int j = 0;
            for (; j < dishes.Count ; ++j)
            {
                if (correctDishesIndexes.Contains(j))
                {
                    continue;
                }
                if((dishesOrdered[i].name+"(Clone)") == dishes[j].name)
                {
                    correctDishesIndexes.Add(j);
                    break;
                }
            }
            if (j == dishes.Count)
            {
                return null;
            }
            if (correctDishesIndexes.Count == dishesOrdered.Count)
            {
                return gameObject;
            }
        }
        return null;
    }

    public void OpenPopup()
    {
        orderPopup.SetActive(true);
    }
}
