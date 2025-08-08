using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public interface ICollider  
{
    public BoxCollider2D Collider { get; set; }

    void SetColliderSizeAndOffset(Vector2 size, Vector2 offset)
    {
        Collider.size = size;
        Collider.offset = offset;
    }
}


