using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EsteiraManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> possibleIngredients;
    [HideInInspector] public List<GameObject> spawnedIngredients = new List<GameObject>();
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
            spawnedIngredients.Add(Instantiate(nextIngredient, spawnpoint));
        }

        for(int i = 0 ; i < spawnedIngredients.Count ; ++i)
        {
            spawnedIngredients[i].GetComponent<Rigidbody2D>().MovePosition(spawnedIngredients[i].transform.position + directionalSpeed);
        }
    }
}
