using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TypeBDropStrategy : DropStrategy
{
    
    public bool ValidateDrop(Collider2D containerCollider, string objectType)
    {
        // Validate the drop for object based on the container's collider and the object's type
        if (objectType == "TypeBDropStrategy" && containerCollider.name=="TypeBBasket")
        {
            return true;
        }
        return false;
    }

    public void HandleDrop(GameObject draggableObject, Collider2D containerCollider)
    {
        // Handle the drop for objects in the appropriate container
        draggableObject.transform.position = containerCollider.transform.position;
    }
}