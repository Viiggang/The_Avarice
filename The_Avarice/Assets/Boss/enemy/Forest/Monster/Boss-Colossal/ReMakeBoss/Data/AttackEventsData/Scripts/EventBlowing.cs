using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static ColossalEvent;

[CreateAssetMenu(fileName = "Blowing", menuName = "Boss/Colossal/CreateAttackEvent/Blowing")]
public class EventBlowing : BaseAniEvent
{
    /*
     1페이지는 밀쳐내기
     2페이지 추가로 체력 비례 10%딜
     */
   
    public Vector2 force;
    BossStage BossStage;
    PlayerMgr player;
    public override void Execute(BossController controller, params object[] data)
    {
        AttackCollisionData collisionData=new();
        
        foreach (var item in data)
        {
            if (item is AttackCollisionData Data)
            {
                collisionData = Data;
            }
            if (item is BossStage stage)
            {
                BossStage = stage;
            }
        }

        var Collider =  Physics2D.OverlapBox(collisionData.offset, collisionData.size,0f, collisionData.playerLayer);
        if (Collider == null) return;
        var rigid= Collider.GetComponent<Rigidbody2D>();
        var Hit = Collider.GetComponent<IDamage>();
        player= Collider.GetComponentInParent<PlayerMgr>();
        if (rigid == null) { Debug.Log("ds"); return; }
        
        rigid.AddForce(force,ForceMode2D.Impulse);
        bool isAttackable = CanAttack();
        if (isAttackable)
        {
            var MaxHp=PlayerMgr.instance.getPlayerMaxHp();
            float finalAttack = MaxHp*0.1f;
            Hit.OnHitDamage(finalAttack);
        }
    }
    private bool CanAttack()
    {
        return BossStage.bossStage == Stage.Stage2 ? true : false;
    }
}
