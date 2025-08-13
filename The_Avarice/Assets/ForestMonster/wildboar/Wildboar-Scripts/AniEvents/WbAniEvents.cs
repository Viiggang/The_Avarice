using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
    [Leein.InspectorName("공격 범위 기즈모 보기")][SerializeField] private bool Gizmosflag = true;
    public void Death()
    {
        Destroy(Boar);
    }
    private void Start()
    {
        Gizmosflag = false;
    }
    private void OnDrawGizmos()
    {
        if (!Gizmosflag) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(manager.BoarTrans.position, manager.statusManager.collider2D.bounds.size);
    }
    public void attack()
    {
        const float left = -1;
        const float right = 1;
        float dirX = manager.WBDetectionrange.renderer.flipX ? left : right;
        rigid.AddForce(new Vector2(dirX * dashForce, jumpForce), ForceMode2D.Impulse);

        var hit=Physics2D.OverlapBox(manager.BoarTrans.position, manager.statusManager.collider2D.bounds.size,0f,player);
        
        if (hit == null) return;
        var damage = hit.GetComponent<IDamage>();

        if (damage == null) return;
        damage.OnHitDamage(1f);
    }
    public void AttackToIdle()
    {
        manager.MonsterMachine.ChangeState(manager.Idle);
    }
   
    
}

[CustomEditor(typeof(WB_AniEvents))]
public class WB_AniEventsEditor : Editor
{
    private bool clicked = false; // 버튼 클릭 상태 저장
    public override void OnInspectorGUI()
    {
        // 기본 인스펙터 그리기
        DrawDefaultInspector();

        WB_AniEvents script = (WB_AniEvents)target;
        string buttonText = clicked ? "호출 완료" : "Test Event 호출";
        // 버튼 추가
        if (GUILayout.Button(buttonText))
        {
           
           clicked = !clicked; // 클릭 후 상태 변경
        }

    }
}
