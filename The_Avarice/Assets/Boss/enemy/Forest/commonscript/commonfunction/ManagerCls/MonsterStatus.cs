using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class MonsterStatus : MonoBehaviour, IDamage
{
    #region 
    //���� �⺻ �ɷ�ġ
    //�ܺ� Ŭ�������� ����ϴ°͵� 
    [Leein.InspectorName("����� ������")][SerializeField] public MonsterData monsterData;
    [Leein.InspectorName("���� ü��")] public float monsterHp;
    [Leein.InspectorName("���� ������")] public float BoarDamage;
    [Leein.InspectorName("���� �̵��ӵ�")] public float movespeed;
    [Leein.InspectorName("���� ���� �ð�")] public float patrolTime;
    [Leein.InspectorName("���� ��� �ð�")] public float IdleTime;
    [Leein.InspectorName("���� ���� �Ÿ�")] public float AttackDistance;
    [Leein.InspectorName("���� ����")] public int defense;
    #endregion

    #region 
    //�ܺ� Ŭ�������� ����ϴ°͵� 
    public float time; //��ٸ� �ð�
    [HideInInspector] public Vector3 moveDir;
    #endregion

    #region 
    //Ŭ���� ���ο����� ���� �͵�
    [SerializeField] public BoxCollider2D BoxCollider2D;
    [SerializeField] public SpriteRenderer spriteRenderer;
    [SerializeField] private Vector2 offsetX;
    private Vector2 defaultOffset;
    [HideInInspector] public bool lockGizmos = false;
    [SerializeField] public MonsterAniController AniManager;
    #endregion


#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (lockGizmos)
        {
            //�ν���Ʈ â���� �ǽð� ������Ʈ ������
            monsterHp = monsterData.Hp;
            BoarDamage = monsterData.Damage;
            movespeed = monsterData.MoveSpeed;
            patrolTime = monsterData.PatrolTime;
            IdleTime = monsterData.IdleTime;
            AttackDistance = monsterData.AttackDistance;
            defense=monsterData.Defense;
        }

    }
#endif
    private void Awake()
    {
        ResetValues();
        lockGizmos = false;
        //Invoke("selfHit", 2f);
    }
    private void Update()
    {
        OffsetCorrection();
    }

    private void OffsetCorrection()
    {
        if (spriteRenderer.flipX)
        {
            BoxCollider2D.offset = defaultOffset;

        }
        else
        {
            //���Ͱ� ���� �� �� �ݶ��̴�  ������ ����
            BoxCollider2D.offset = defaultOffset + offsetX;
        }
    }
    private void ResetValues()
    {
        AttackDistance = monsterData.AttackDistance;
        monsterHp = monsterData.Hp;
        BoarDamage = monsterData.Damage;
        movespeed = monsterData.MoveSpeed;
        patrolTime = monsterData.PatrolTime;
        IdleTime = monsterData.IdleTime;
        defaultOffset = BoxCollider2D.offset;
        defense = monsterData.Defense;
        time = 0;
    }
    public void OnHitDamage(float Damage)
    {

        monsterHp -= Damage;
        if (monsterHp <=0)
        {
            movespeed = 0;
            AniManager.Play("death");
        }
    }
    [ContextMenu("Hit")]
    public void selfHit()
    {
        OnHitDamage(1000);
    }
}

[CustomEditor(typeof(MonsterStatus))]
public class ApplyRealTimeStatus : Editor
{
    private bool button = false;
    public override void OnInspectorGUI()
    {

        DrawDefaultInspector();
        var data = (MonsterStatus)target;
        string ButtonName = data.lockGizmos ? "�ǽð� ������ ����ON" : "�ǽð� ������ ����Off";
        if (GUILayout.Button(ButtonName))
        {
            data.lockGizmos = !button;
            button = !button;
            SceneView.RepaintAll();
        }
       
    }
}

