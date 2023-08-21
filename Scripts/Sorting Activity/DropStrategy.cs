using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface DropStrategy
{
    bool ValidateDrop(Collider2D containerCollider, string objectType);
    void HandleDrop(GameObject draggableObject, Collider2D containerCollider);
}


