using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WBDetectionrange : MonoBehaviour
{
    [SerializeField]private Collider2D BoarCollider;
    [SerializeField]private Vector3 size;
    [SerializeField]private Vector3 right;
    [SerializeField]public SpriteRenderer renderer;
    [SerializeField]private LayerMask playerLayer;
    [SerializeField]public Collider2D findcollider;

    private Vector3 detectionCenter;
    private Vector3 center;
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
         Gizmos.color = Color.red;
        if (BoarCollider == null) return;
        if (renderer == null) return;

          center = BoarCollider.bounds.center;

        detectionCenter = renderer.flipX? center - right : center + right;
        Gizmos.DrawWireCube(detectionCenter, size);
        var hit = Physics2D.OverlapBox(detectionCenter, size,0f,playerLayer);
        if (hit == null)
        {
            findcollider = null;
            return;
        }
        findcollider = hit;
    }
#endif
    private void Start()
    {
          center = BoarCollider.bounds.center;
    }
    private void Update()
    {
        detectionCenter = renderer.flipX ? center - right : center + right;
        var hit = Physics2D.OverlapBox(detectionCenter, size, 0f, playerLayer);
        if (hit == null)
        {
            findcollider = null;
            return;
        }
        findcollider = hit;
    }

}
