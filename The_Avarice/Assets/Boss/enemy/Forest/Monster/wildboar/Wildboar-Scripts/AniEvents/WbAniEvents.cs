using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class WB_AniEvents : MonoBehaviour
{
    [Leein.InspectorName("죽었을 때 지울 자신(최상위 오브젝트)")][SerializeField]private GameObject Boar;
    [Leein.InspectorName("몬스터 rigidbody 주입")][SerializeField] private Rigidbody2D rigid;
    [Leein.InspectorName("몬스터 매니저 주입")][SerializeField] private MonsterController manager;
    [Leein.InspectorName("플레이어 Layer로 설정")][SerializeField] private LayerMask player;//찾을 레이어
   [Leein.InspectorName("돌진 힘")] public float dashForce; // 돌진 힘
   [Leein.InspectorName("점프 힘")] public float jumpForce;//// 점프 힘
    private bool HitFlag = false;
    [HideInInspector]public bool Gizmosflag=false;
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
        Gizmos.DrawWireCube(manager.MonsterTrans.position, manager.statusManager.BoxCollider2D.bounds.size);
    }
    public void attack()
    {
        const float left = -1;
        const float right = 1;
        float dirX = manager.Detection.renderer.flipX ? left : right;
        rigid.AddForce(new Vector2(dirX * dashForce, jumpForce), ForceMode2D.Impulse);

        var hit=Physics2D.OverlapBox(manager.MonsterTrans.position, manager.statusManager.BoxCollider2D.bounds.size,0f,player);
        
        if (hit == null) return;
        var damage = hit.GetComponent<IDamage>();

        if (damage == null) return;
        damage.OnHitDamage(1f);
    }
    public void AttackToIdle()
    {
        Dictionary<string, MonsterStates> WildBoarState= manager.State;
        string CurrentState= manager.StartState;
        MonsterMachine<MonsterController> MonsterMachine= manager.MonsterMachine;
       
        MonsterMachine.ChangeState(WildBoarState[CurrentState], manager);
      
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

        WB_AniEvents obj = (WB_AniEvents)target;
        string buttonText = obj.Gizmosflag ? "공격범위 기즈모 활성화 상태" : "공격범위 기즈모 비활성화 상태";
        // 버튼 추가
        if (GUILayout.Button(buttonText))
        {
            obj.Gizmosflag = !clicked;
            clicked = !clicked; // 클릭 후 상태 변경
            SceneView.RepaintAll();
        }
        
    }

}
