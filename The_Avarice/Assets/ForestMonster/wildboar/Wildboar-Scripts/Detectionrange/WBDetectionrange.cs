using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
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
    private Vector2 scaledSize;
    private Vector3 scaledRight;
   [SerializeField]private Transform wildbar;
    [SerializeField]private bool gizmos=true;
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (!gizmos) return;
         Gizmos.color = UnityEngine.Color.red;
        if (BoarCollider == null) return;
        if (renderer == null) return;

        center = BoarCollider.bounds.center;
        
         scaledSize = new Vector2(
        size.x * transform.lossyScale.x,
        size.y * transform.lossyScale.y
         );
        
        // 로컬 오프셋 right에 스케일 반영
         scaledRight = new Vector3(
            right.x * transform.lossyScale.x,
            right.y * transform.lossyScale.y,
            right.z * transform.lossyScale.z
        );

        // detectionCenter 계산 (월드 좌표)
        detectionCenter = renderer.flipX ? (center - scaledRight) : (center + scaledRight);

        Gizmos.DrawWireCube(detectionCenter, scaledSize);
        var hit = Physics2D.OverlapBox(detectionCenter, scaledSize, 0f,playerLayer);
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
        scaledSize = new Vector2(
         size.x * transform.lossyScale.x,
        size.y * transform.lossyScale.y
         );

        // 로컬 오프셋 right에 스케일 반영
        scaledRight = new Vector3(
           right.x * transform.lossyScale.x,
           right.y * transform.lossyScale.y,
           right.z * transform.lossyScale.z
            );
    }
    private void Update()
    {

        center = BoarCollider.bounds.center;
        detectionCenter = renderer.flipX ? (center - scaledRight) : (center + scaledRight);
        var hit   = Physics2D.OverlapBox(detectionCenter, scaledSize, 0f, playerLayer);
        if (hit == null)
        {
            findcollider = null;
            return;
        }
        findcollider = hit;
    }

}
