using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Order : MonoBehaviour
{
    private Ingredient dishOrdered = new ();
    [SerializeField] private GameObject orderTimerRef;
    [SerializeField] private Image dishImage;
    [SerializeField] private List<Sprite> sheetSprites;
    private Image orderTimer;
    private float waitTimer;
    public float maxWaitTime;
    private int score;
    public int Score => score;

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
            transform.parent.parent.GetComponent<OrderManager>().FailOrder(this);
        }
        orderTimer.fillAmount = waitTimer / maxWaitTime;
    }

    public void MakeAnOrder(List<Ingredient> possibleDishes)
    {
        dishOrdered = possibleDishes[Random.Range(0, possibleDishes.Count)];
        score = dishOrdered.pointsReward;
        dishImage.sprite = dishOrdered.Sprite;
    }

    public bool CheckIfDishCompletesOrder(Ingredient dish)
    {
        return dish.Id.Equals(dishOrdered.Id);
    }
}