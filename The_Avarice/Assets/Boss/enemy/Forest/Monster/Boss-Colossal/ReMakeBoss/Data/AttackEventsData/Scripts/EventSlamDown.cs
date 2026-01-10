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
       
        foreach (var item in data)
        {
            if (item is AttackCollisionData Data)
            {
                collisionData = Data;
            }

        }
        var Collider = Physics2D.OverlapBox(collisionData.offset, collisionData.size, 0f, collisionData.playerLayer);
        if (Collider == null) return;
        Debug.Log($"EventSlamDown : {Collider.name}");
        var Hit = Collider.GetComponentInChildren<IDamage>();
       
        if (Hit == null) return;
        var PlayerMaxHP = PlayerMgr.instance.MaxHp;
        float finallDamage = (PlayerMaxHP * 0.3f) * (-1f);
        Hit.OnHitDamage(finallDamage);
        ////기절 어떻게 구현??
    }
}

