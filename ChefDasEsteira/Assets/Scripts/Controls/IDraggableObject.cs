using System;
using UnityEngine;

public interface IDraggableObject
{
    void MoveToPosition(Vector2 position);
    void OnStartDrag(Action<bool> OnFinishDragCallback = null);
    void OnFinishDrag(bool releasedOnValidPosition);
}