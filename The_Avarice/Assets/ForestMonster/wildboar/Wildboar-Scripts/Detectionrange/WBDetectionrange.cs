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
    [SerializeField]private bool gizmos=true;
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (!gizmos) return;
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
         gizmos = false;
         
    }
    private void Update()
    {
        center = BoarCollider.bounds.center;
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
