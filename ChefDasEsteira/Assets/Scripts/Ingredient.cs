using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public enum ingredientNames { Salmon, Tuna, Rice, Algae }
    public ingredientNames ingredientName;
    [HideInInspector] public float moveSpeed;
    private Vector3 directionalSpeed;
    [SerializeField] private Rigidbody2D rb2d;

    // Start is called before the first frame update
    void Start()
    {
        directionalSpeed = new Vector3(moveSpeed, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        rb2d.MovePosition(transform.position + directionalSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "fim esteira")
        {
            Destroy(this.gameObject);
        }
    }
}
