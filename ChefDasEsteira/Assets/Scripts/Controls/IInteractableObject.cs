using System;
using UnityEngine;

public interface IInteractableObject
{
    bool CanReceiveOtherObjects { get; }
    IInteractableObject OnObjectClicked();
    void ObjectClickReleased(IInteractableObject objectReleasedUpon);
}