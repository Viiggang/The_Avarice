using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class MonsterStatus : MonoBehaviour, IDamage
{
    #region 
    //몬스터 기본 능력치
    //외부 클래스에서 사용하는것들 
    [Leein.InspectorName("사용할 데이터")][SerializeField] public MonsterData monsterData;
    [Leein.InspectorName("몬스터 체력")] public float monsterHp;
    [Leein.InspectorName("몬스터 데미지")] public float BoarDamage;
    [Leein.InspectorName("몬스터 이동속도")] public float movespeed;
    [Leein.InspectorName("몬스터 순찰 시간")] public float patrolTime;
    [Leein.InspectorName("몬스터 대기 시간")] public float IdleTime;
    [Leein.InspectorName("몬스터 공격 거리")] public float AttackDistance;
    [Leein.InspectorName("몬스터 방어력")] public int defense;
    #endregion

    #region 
    //외부 클래스에서 사용하는것들 
    public float time; //기다린 시간
    [HideInInspector] public Vector3 moveDir;
    #endregion

    #region 
    //클래스 내부에서만 쓰는 것들
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
            //인스펙트 창에서 실시간 업데이트 보려서
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
            //몬스터가 우측 볼 때 콜라이더  오프셋 보정
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
        string ButtonName = data.lockGizmos ? "실시간 데이터 적용ON" : "실시간 데이터 적용Off";
        if (GUILayout.Button(ButtonName))
        {
            data.lockGizmos = !button;
            button = !button;
            SceneView.RepaintAll();
        }
       
    }
}

