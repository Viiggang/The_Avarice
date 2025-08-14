using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.Rendering.DebugUI;

public class BoarStatus : MonoBehaviour,IDamage
{
    [Leein.InspectorName("사용할 데이터")][SerializeField]private MonsterData monsterData;
    [Leein.InspectorName("맷돼지 체력")]public float BoarHp;
    [Leein.InspectorName("맷돼지 데미지")]public float BoarDamage;
    [Leein.InspectorName("맷돼지 이동속도")]public float movespeed;
    [Leein.InspectorName("맷돼지 순찰 시간")] public float patrolTime;
    [Leein.InspectorName("맷돼지 대기 시간")] public float IdleTime;
    [Leein.InspectorName("맷돼지 공격 거리")] public float AttckDistance;
    [SerializeField] public BoxCollider2D collider2D;
    [SerializeField]private SpriteRenderer spriteRenderer;
    [SerializeField]private Vector2 offsetX;
    private Vector2 defaultOffset;

    [SerializeField] private BoarAniManager BoarAniManager;
    [SerializeField] private bool lockGizmos = true;
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if(lockGizmos)
        {
            //인스펙트 창에서 실시간 업데이트 보려서
            BoarHp = monsterData.Hp;
            BoarDamage = monsterData.Damage;
            movespeed = monsterData.MoveSpeed;
            patrolTime = monsterData.PatrolTime;
            IdleTime = monsterData.IdleTime;
            AttckDistance = monsterData.AttackDistance;
           
        }
       
    }
#endif
    private void Start()
    {
        lockGizmos = false;
        AttckDistance = monsterData.AttackDistance;
        BoarHp = monsterData.Hp;
        BoarDamage = monsterData.Damage;
        movespeed = monsterData.MoveSpeed;
        patrolTime = monsterData.PatrolTime;
        IdleTime = monsterData.IdleTime;
        defaultOffset = collider2D.offset;
    }
    private void Update()
    {
        if (spriteRenderer.flipX)
        {
            collider2D.offset = defaultOffset;

        }
        else
        {
            collider2D.offset = defaultOffset + offsetX;
        }
    }
    public void OnHitDamage(float Damage)
    {

        BoarHp -= Damage;
        if (BoarHp <0)
        {
            BoarAniManager.Play_Death();
        }
    }
}
 