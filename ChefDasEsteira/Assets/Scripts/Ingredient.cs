using System;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour, IDraggableObject, IDraggableObjectProvider
{
    [SerializeField] private string id;
    public List<Ingredient> requiredIngredients;
    public int pointsReward;

    public string Id => id;
    
    private Vector2 originalPos;
    private Action<bool> OnDragFinished;
    
    private Collider2D thisCollider2d;
    
    private void Awake()
    {
        thisCollider2d = GetComponent<Collider2D>();
    }

    public void SetNewParentContainer(Transform parentContainer)
    {
        Transform thisTransform;
        (thisTransform = transform).SetParent(parentContainer, false);
        thisTransform.localPosition = new Vector3(0, 0, -1);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "fim esteira")
        {
            Destroy(this.gameObject);
        }
    }

    public void MoveToPosition(Vector2 position)
    {
        transform.position = position;
    }

    public void OnStartDrag(Action<bool> onFinishDragCallback = null)
    {
        OnDragFinished = onFinishDragCallback;
        originalPos = transform.position;
        
        thisCollider2d.enabled = false;
    }

    public void OnFinishDrag(bool releasedOnValidPosition)
    {
        if (!releasedOnValidPosition)
        {
            transform.position = originalPos;
            thisCollider2d.enabled = true;
        }
        
        OnDragFinished?.Invoke(releasedOnValidPosition);
    }

    public bool TryGetDraggableObject(out IDraggableObject obj, out Action<bool> onDragFinishedCallback)
    {
        onDragFinishedCallback = null;
        obj = this;
        return true;
    }
}