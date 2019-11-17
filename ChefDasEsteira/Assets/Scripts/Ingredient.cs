using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public enum names { Salmao, Atum, Camarao, Arroz, Alga,
                                SalmaoFatiado, AtumFatiado, CamaraoFatiado, ArrozCozido }
    public names ingredientName;
    public bool isInEsteira = true;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "fim esteira")
        {
            RemoveFromEsteira();
            Destroy(this.gameObject);
        }
    }

    public void RemoveFromEsteira()
    {
        isInEsteira = false;
        transform.parent.parent.GetComponent<EsteiraManager>().spawnedIngredients.Remove(gameObject);
    }
}
