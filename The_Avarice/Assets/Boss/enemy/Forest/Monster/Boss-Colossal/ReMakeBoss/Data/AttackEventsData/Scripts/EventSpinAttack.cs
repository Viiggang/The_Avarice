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
        PlayerMgr player;
        foreach (var item in data)
        {
            if (item is AttackCollisionData Data)
            {
                collisionData = Data;
            }

        }
        var Collider = Physics2D.OverlapBox(collisionData.offset, collisionData.size, 0f, collisionData.playerLayer);
        
      
        Debug.Log($"{collisionData.playerLayer}&{collisionData.playerLayer.ToString()}");
        if (Collider == null) return;
        var Hit = Collider.GetComponent<IDamage>();
        player = Collider.GetComponentInParent<PlayerMgr>();
        if (Hit == null || player == null) return;
        var PlayerMaxHP = player.getPlayerMaxHp();
        float finallDamage = PlayerMaxHP*0.1f;
        Hit.OnHitDamage(finallDamage);
    }
}
