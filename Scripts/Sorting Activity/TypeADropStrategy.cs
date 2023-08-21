using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeADropStrategy : DropStrategy
{
    public bool ValidateDrop(Collider2D containerCollider, string objectType)
    {
        // Validate the drop for fruits based on the container's collider and the object's type
        if (objectType == "TypeADropStrategy" && containerCollider.name=="TypeABasket")
        {
            return true;
        }
        return false;
    }

    public void HandleDrop(GameObject draggableObject, Collider2D containerCollider)
    {
        // Handle the drop for fruits in the appropriate container
       draggableObject.transform.position = containerCollider.transform.position;
    }
}
