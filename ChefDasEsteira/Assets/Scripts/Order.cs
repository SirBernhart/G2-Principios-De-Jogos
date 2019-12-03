using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Order : MonoBehaviour
{
    private List<GameObject> dishesOrdered = new List<GameObject>();
    [SerializeField] private GameObject orderTimerRef;
    [SerializeField] private List<Sprite> sheetSprites;
    private Image orderTimer;
    private float waitTimer;
    public float maxWaitTime;
    public int totalScore;

    private void Start()
    {
        orderTimer = orderTimerRef.GetComponent<Image>();
        waitTimer = maxWaitTime;
    }

    private void Update()
    {
        waitTimer -= Time.deltaTime;
        if(waitTimer <= 0)
        {
            transform.parent.parent.GetComponent<OrderManager>().FailOrder(gameObject);
        }
        orderTimer.fillAmount = waitTimer / maxWaitTime;
    }

    public void MakeAnOrder(List<GameObject> possibleDishes)
    {
        int numberOfDishes = 1;

        for(int i = 0 ; i < numberOfDishes ; ++i)
        {
            dishesOrdered.Add(possibleDishes[Random.Range(0, possibleDishes.Count)]);
            totalScore += dishesOrdered[dishesOrdered.Count - 1].GetComponent<Ingredient>().pointsReward;
        }
        for(int i = 0 ; i < sheetSprites.Count ; ++i)
        {
            if (sheetSprites[i].name == dishesOrdered[0].name)
                transform.GetChild(0).GetComponent<Image>().sprite = sheetSprites[i];
        }
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
}
