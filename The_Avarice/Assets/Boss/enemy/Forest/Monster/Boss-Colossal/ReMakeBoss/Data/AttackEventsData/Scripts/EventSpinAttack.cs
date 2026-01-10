using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SpinAttack", menuName = "Boss/Colossal/CreateAttackEvent/SpinAttack")]
public class EventSpinAttack : BaseAniEvent
{
    //Ã¼·Â ºñ·Ê 10%
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
        Debug.Log($"EventSpinAttack : {Collider.name}");
        var Hit = Collider.GetComponentInChildren<IDamage>();
        
        if (Hit == null ) return;
        var PlayerMaxHP = PlayerMgr.instance.MaxHp;
        float finallDamage = (PlayerMaxHP * 0.1f)*(-1f);
        Hit.OnHitDamage(finallDamage);
    }
}
