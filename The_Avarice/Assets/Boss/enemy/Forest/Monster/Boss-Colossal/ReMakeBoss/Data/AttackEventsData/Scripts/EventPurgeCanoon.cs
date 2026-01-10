using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Canoon", menuName = "Boss/Colossal/CreateAttackEvent/Canoon")]
public class EventPurgeCanoon : BaseAniEvent
{
    /*
     최대 체력 100%
     */

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
        Debug.Log($"EventPurgeCanoon : {Collider.name}");
        var Hit = Collider.GetComponentInChildren<IDamage>();
      
        if (Hit ==null  ) return;
        var PlayerMaxHP = PlayerMgr.instance.MaxHp;
        float finallDamage = PlayerMaxHP;
        Hit.OnHitDamage(-finallDamage);
    }
}
