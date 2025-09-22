using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class WB_AniEvents : MonoBehaviour
{
    [Leein.InspectorName("�׾��� �� ���� �ڽ�(�ֻ��� ������Ʈ)")][SerializeField]private GameObject Boar;
    [Leein.InspectorName("���� rigidbody ����")][SerializeField] private Rigidbody2D rigid;
    [Leein.InspectorName("���� �Ŵ��� ����")][SerializeField] private MonsterController manager;
    [Leein.InspectorName("�÷��̾� Layer�� ����")][SerializeField] private LayerMask player;//ã�� ���̾�
   [Leein.InspectorName("���� ��")] public float dashForce; // ���� ��
   [Leein.InspectorName("���� ��")] public float jumpForce;//// ���� ��
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
    private bool clicked = false; // ��ư Ŭ�� ���� ����
    public override void OnInspectorGUI()
    {
        // �⺻ �ν����� �׸���
        DrawDefaultInspector();

        WB_AniEvents obj = (WB_AniEvents)target;
        string buttonText = obj.Gizmosflag ? "���ݹ��� ����� Ȱ��ȭ ����" : "���ݹ��� ����� ��Ȱ��ȭ ����";
        // ��ư �߰�
        if (GUILayout.Button(buttonText))
        {
            obj.Gizmosflag = !clicked;
            clicked = !clicked; // Ŭ�� �� ���� ����
            SceneView.RepaintAll();
        }
        
    }

}
