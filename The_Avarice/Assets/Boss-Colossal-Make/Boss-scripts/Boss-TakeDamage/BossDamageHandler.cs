using Boss_Colossal;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDamageHandler : MonoBehaviour
{
    [SerializeField]private BossAblity BossAblity;
    
    private void TakeDamage(float Damage)
    {
        BossAblity.Hp -= Damage;
    }
    private void CheckDeath()
    {
       bool isDead=false;
       isDead = BossAblity.Hp < 0;
        
        if(isDead)
        {
            BossController.Instance.ChangeState(new DeadState());
        }
    }

    public void  ApplyDamage(float Damage)
    {
        TakeDamage(Damage);
        CheckDeath();
    }


}
