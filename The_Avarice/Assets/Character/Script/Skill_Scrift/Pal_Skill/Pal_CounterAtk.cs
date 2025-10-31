using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pal_CounterAtk : MonoBehaviour
{
    float AtkDamage = 5f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamage damage = other.GetComponent<IDamage>();

        if (damage != null && other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            damage.OnHitDamage(AtkDamage);
            if (PlayerMgr.instance.getPlayerType() == Player_Type.Paladin && !PlayerMgr.instance.getonPassive())
            {
                PlayerMgr.instance.sumPassiveStack(2);
            }
        }

    }
}
