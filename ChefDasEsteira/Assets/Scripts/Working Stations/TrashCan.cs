using UnityEngine;

public class TrashCan : MonoBehaviour, IDraggableObjectReceiver
{
    public bool TryReceiveDraggableObject(IDraggableObject obj)
    {
        Ingredient ingredient = obj as Ingredient; 
        
        Destroy(ingredient.gameObject);
        return true;
    }
}