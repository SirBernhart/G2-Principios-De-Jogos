using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EsteiraManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> possibleIngredients;
    public float timeBetweenIngredients;
    [SerializeField] private float moveSpeed;
    private Vector3 directionalSpeed;
    [SerializeField] private Transform spawnpoint;

    private void Start()
    {
        timerNextIngredient = timeBetweenIngredients;
        directionalSpeed = new Vector3(moveSpeed, 0, 0);
    }

    private float timerNextIngredient;
    void Update()
    {
        timerNextIngredient += Time.deltaTime;
        if(timerNextIngredient >= timeBetweenIngredients)
        {
            timerNextIngredient = 0;

            GameObject nextIngredient = possibleIngredients[Random.Range(0, possibleIngredients.Count)];
            Instantiate(nextIngredient, spawnpoint);
        }

        for(int i = 0 ; i < spawnpoint.childCount ; ++i)
        {
            spawnpoint.GetChild(i).GetComponent<Rigidbody2D>().MovePosition(spawnpoint.GetChild(i).transform.position + directionalSpeed * Time.deltaTime);
        }
    }
}
