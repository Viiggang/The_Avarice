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
    [SerializeField] public BoxCollider2D collider2D;
    [SerializeField]private SpriteRenderer spriteRenderer;
    [SerializeField]private Vector2 offsetX;
    private Vector2 defaultOffset;

   [SerializeField] private bool lockOffset = false;
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        //인스펙트 창에서 실시간 업데이트 보려서
        BoarHp = monsterData.Hp;
        BoarDamage = monsterData.Damage;
        movespeed = monsterData.MoveSpeed;
        patrolTime = monsterData.PatrolTime;
        IdleTime = monsterData.IdleTime;
    }
#endif
    private void Start()
    {
        BoarHp = monsterData.Hp;
        BoarDamage = monsterData.Damage;
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
        monsterData.Hp = Damage;
        BoarHp = monsterData.Hp;
    }
}
 