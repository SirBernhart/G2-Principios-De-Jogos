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
            LeaveCurrentSpace();
            Destroy(this.gameObject);
        }
    }

    public void LeaveCurrentSpace()
    {
        if (isInEsteira)
        {
            isInEsteira = false;
            transform.parent.parent.GetComponent<EsteiraManager>().spawnedIngredients.Remove(gameObject);
        }
        /*else
        {
            //CuttingBoard
            //DishManager
            DishManager dishManagerRef = transform.parent.GetComponent<DishManager>();
            if(dishManagerRef != null)
            {
                dishManagerRef.RemoveElement();
            }
            //Plate
        }*/
    }
}
