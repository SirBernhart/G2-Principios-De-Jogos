using System;

public interface IDraggableObjectProvider
{
    bool TryGetDraggableObject(out IDraggableObject obj, out Action<bool> onDragFinishedCallback);
}