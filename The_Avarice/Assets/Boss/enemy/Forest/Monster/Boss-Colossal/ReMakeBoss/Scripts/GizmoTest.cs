using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmoTest :Singleton<GizmoTest>
{
    public Vector2 offset;
    public Vector2 size;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(offset, size);
    }
    private void Awake()
    {
        base.Awake();
    }
    public void Set(Vector2 offset, Vector2 size)
    {
        this.offset = offset;
        this.size = size;
    }
}
