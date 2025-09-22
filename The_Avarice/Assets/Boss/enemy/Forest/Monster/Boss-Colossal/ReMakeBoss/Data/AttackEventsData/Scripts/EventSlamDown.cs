using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SlamDown", menuName = "Boss/Colossal/CreateAttackEvent/SlamDown")]
public class EventSlamDown : BaseAniEvent
{
    //체력 비례 30% 2초 기절
    public override void Execute(BossController controller, params object[] data)
    {
        AttackCollisionData collisionData = new();
        PlayerMgr player;
        foreach (var item in data)
        {
            if (item is AttackCollisionData Data)
            {
                collisionData = Data;
            }

        }
        var Collider = Physics2D.OverlapBox(collisionData.offset, collisionData.size, 0f, collisionData.playerLayer);
        if (Collider == null) return;
        var Hit = Collider.GetComponent<IDamage>();
        player = Collider.GetComponentInParent<PlayerMgr>();
        if (Hit == null || player == null) return;
        var PlayerMaxHP = player.getPlayerMaxHp();
        float finallDamage = PlayerMaxHP*0.3f;
        Hit.OnHitDamage(finallDamage);
        //기절 어떻게 구현??
    }
}

