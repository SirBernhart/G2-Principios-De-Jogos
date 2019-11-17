using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dish Name", menuName = "New Dish")]
public class Dish : ScriptableObject
{
    [SerializeField] private List<GameObject> requiredIngredients;
}
