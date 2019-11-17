using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EsteiraManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> possibleIngredients;
    private List<GameObject> spawnedIngredients = new List<GameObject>();
    public float timeBetweenIngredients;
    [SerializeField] private float moveSpeed;
    private Vector3 directionalSpeed;
    [SerializeField] private Transform spawnpoint;

    private void Start()
    {
        timerNextIngredient = timeBetweenIngredients;
    }

    private float timerNextIngredient;
    void Update()
    {
        timerNextIngredient += Time.deltaTime;
        if(timerNextIngredient >= timeBetweenIngredients)
        {
            timerNextIngredient = 0;

            GameObject nextIngredient = possibleIngredients[Random.Range(0, possibleIngredients.Count)];
            Instantiate(nextIngredient, spawnpoint).GetComponent<Ingredient>().moveSpeed = moveSpeed;
        }
    }
}
