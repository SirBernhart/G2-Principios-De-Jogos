using System;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    [SerializeField] RectTransform preparationTableButton;
    private bool isCountingDoubleClick;
    private IDraggableObject currentHeldObject;
    private Vector2 heldGameObjectOriginalPosition;
    private Camera mainCamera;
    private Action<bool> lastDraggableObjProviderCallback;

    private bool isDragging;

    // Souns
    [SerializeField] private AudioSource lixo;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryGetADraggableObjectUnderPointer();
        }
        if (Input.GetMouseButton(0))
        {
            if(currentHeldObject != null && 
               Vector2.Distance(heldGameObjectOriginalPosition, new Vector2(Input.mousePosition.x, Input.mousePosition.y)) > 10)
            {
                if (!isDragging)
                {
                    currentHeldObject.OnStartDrag(lastDraggableObjProviderCallback);
                    isDragging = true;
                }
                
                currentHeldObject.MoveToPosition(GetMousePos2D());
            }
        }
        else if (Input.GetMouseButtonUp(0) && currentHeldObject != null)
        {
            isDragging = false;

            if (!TryPlaceDraggableObjectInReceiverUnderPointer())
            {
                currentHeldObject.OnFinishDrag(false);
                return;
            }
            
            currentHeldObject.OnFinishDrag(true);
            //TODO: add trash behaviour again
            
            currentHeldObject = null;
        }
    }
    
    private bool TryGetADraggableObjectUnderPointer()
    {
        return TryGetComponentOfTypeUnderPointer<IDraggableObjectProvider>(out var draggableObjReceiver) && 
               draggableObjReceiver.TryGetDraggableObject(out currentHeldObject, out lastDraggableObjProviderCallback);
    }

    private bool TryPlaceDraggableObjectInReceiverUnderPointer()
    {
        return TryGetComponentOfTypeUnderPointer<IDraggableObjectReceiver>(out var draggableObjReceiver) && 
               draggableObjReceiver.TryReceiveDraggableObject(currentHeldObject);
    }

    private bool TryGetComponentOfTypeUnderPointer<T>(out T component)
    {
        Vector2 mousePos2D = GetMousePos2D();
        RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector3.zero);

        if (hit.collider)
        {
            component = hit.transform.GetComponent<T>();
            
            return component != null;
        }

        component = default;
        return false;
    }

    private Vector2 GetMousePos2D()
    {
        return mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, mainCamera.nearClipPlane));
    }
}