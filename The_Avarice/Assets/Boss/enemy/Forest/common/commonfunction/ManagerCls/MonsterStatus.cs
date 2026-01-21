using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements.Experimental;
public class MonsterStatus : MonoBehaviour, IDamage
{
    /*기본적으로 몬스터의 상태를 관리하는 클래스 입니다.
      체력/이동 속도/공격력 대미지 피격 관련 처리 를 관리하는 클래스입니다.

    */
    #region 
    //몬스터 기본 능력치
    //외부 클래스에서 사용하는것들 
    [Leein.InspectorName("사용할 데이터")][SerializeField] public MonsterData monsterData;
    [Leein.InspectorName("몬스터 체력")][SerializeField] private float monsterhp;
    [Leein.InspectorName("몬스터 데미지")][SerializeField] private float monsterdamage;
    [Leein.InspectorName("몬스터 이동속도")][SerializeField] private float movespeed;
    [Leein.InspectorName("몬스터 순찰 시간")][SerializeField] private float patroltime;
    [Leein.InspectorName("몬스터 대기 시간")][SerializeField] private float idletime;
    [Leein.InspectorName("몬스터 공격 거리")][SerializeField] private float attackdistance;
    [Leein.InspectorName("몬스터 방어력")][SerializeField] private float defense;
    
    public Action OnDead;
    private bool isfacingleft;
    #endregion
    #region
    public float MonsterHp 
    { 
        get => monsterhp;
        set
        {
            monsterhp = value;
            bool isDead = monsterhp <= 0;
            if (isDead)
            {
                OnDead?.Invoke();
                movespeed = 0;
                AniManager.Play("death");
            }
        }   
    }

    public float MonsterDamage
    {
        get => monsterdamage;
        set
        {
            monsterdamage = value;
        }
    }

    public float MoveSpeed
    {
        get => movespeed;
        set
        {
            movespeed = value;
        }
    }

    public float PatrolTime
    {
        get => patroltime;
        set
        {
            patroltime = value;
        }
    }

    public float IdleTime
    {
        get => idletime;
        set
        {
            idletime = value;
        }
    }

    public float AttackDistance
    {
        get => attackdistance;
        set
        {
            attackdistance = value;
        }
    }

    public float Defense 
    { 
        get => defense;
        set
        {
            defense = value;
        }
    }

    public bool isFacingleft
    {
        get => isfacingleft;
        set
        {
            isfacingleft = value;
            BoxCollider2D.offset = isfacingleft ? defaultOffset : defaultOffset + offsetX; ;
        }
    }
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
            MonsterHp = monsterData.Hp;
            MonsterDamage = monsterData.Damage;
            MoveSpeed = monsterData.MoveSpeed;
            PatrolTime = monsterData.PatrolTime;
            IdleTime = monsterData.IdleTime;
            AttackDistance = monsterData.AttackDistance;
            defense=monsterData.Defense;
        }

    }
#endif
    private void Awake()
    {
        //데이터 복사 후 집어넣기
        monsterData=Instantiate(monsterData);

        ResetValues();
        lockGizmos = false;
  
    }

    private void Update() => OffsetCorrection();

    private void OffsetCorrection() => isFacingleft = spriteRenderer.flipX;
  
    private void ResetValues()//스크립트 오브젝트에 있는 몬스터 데이터로 초기화 한다.
    {
        AttackDistance = monsterData.AttackDistance;
        MonsterHp = monsterData.Hp;
        MonsterDamage = monsterData.Damage;
        movespeed = monsterData.MoveSpeed;
        PatrolTime = monsterData.PatrolTime;
        IdleTime = monsterData.IdleTime;
        defaultOffset = BoxCollider2D.offset;
        defense = monsterData.Defense;
        time = 0;
    }
    //피격 당했을 때 사용되는 함수
    public void OnHitDamage(float Damage) => MonsterHp += -Damage;

    #region
    [ContextMenu("Hit")]//테스트용 코드이다.
    public void selfHit() => OnHitDamage(1000);
    #endregion
}

#if UNITY_EDITOR
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
#endif
