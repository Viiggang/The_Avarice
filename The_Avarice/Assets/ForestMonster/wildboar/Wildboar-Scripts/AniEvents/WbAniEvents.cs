using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class WB_AniEvents : MonoBehaviour
{
    [SerializeField]private GameObject Boar;
    [SerializeField] private Rigidbody2D rigid;
    [SerializeField] private WildBoarManager manager;
    [SerializeField] private LayerMask player;
   [Leein.InspectorName("돌진 힘")] public float dashForce; // 돌진 힘
   [Leein.InspectorName("점프 힘")] public float jumpForce;//// 점프 힘
    private bool HitFlag = false;
    [Leein.InspectorName("공격 범위 기즈모 보기")][SerializeField] private bool Gizmosflag = false;
    public void Death()
    {
        Destroy(Boar);
    }

    private void OnDrawGizmos()
    {
        if (!Gizmosflag) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(manager.BoarTrans.position, manager.statusManager.collider2D.bounds.size);
    }
    public void attack()
    {
        float dirX = manager.WBDetectionrange.renderer.flipX ? -1f : 1f;
        rigid.AddForce(new Vector2(dirX * dashForce, jumpForce), ForceMode2D.Impulse);
        var hit=Physics2D.OverlapBox(manager.BoarTrans.position, manager.statusManager.collider2D.bounds.size,0f,player);
        if (hit == null) return;
        var damage = hit.GetComponent<IDamage>();
        if (damage == null) return;
        damage.OnHitDamage(1f);
    }
   
    
}
