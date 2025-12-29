using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PurgeBlow", menuName = "Boss/Colossal/CreateAttackEvent/purgeblow")]
public class EventPurgeblow : BaseAniEvent
{
    /*
     Ã¼·Â 50%
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
         Debug.Log($"EventPurgeblow : {Collider.name}");
        var Hit = Collider.GetComponentInChildren<IDamage>();
       
        if (Hit == null  ) return;
        var PlayerMaxHP = PlayerMgr.instance.MaxHp;
        float finallDamage = (PlayerMaxHP * 0.5f) * (-1f);

        Hit.OnHitDamage(finallDamage);
    }
}
