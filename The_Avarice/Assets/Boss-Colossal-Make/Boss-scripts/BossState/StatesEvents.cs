using Boss_Colossal;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StatesEvents : MonoBehaviour
{
     public void  IdleState()
     {
        BossController.Instance.ChangeState(new IdleState());
       
     }
    public void WakeState()
    {
        BossController.Instance.ChangeState(new WakeState());
    }
    public void SuperAttackState()
    {
        BossController.Instance.ChangeState(new AttackState());
    }
    public void MeleeAttackState()
    {
        BossController.Instance.ChangeState(new MeleeAttack());
    }
    public void RangeAttackState()
    {
        BossController.Instance.ChangeState(new RangeAttack());
    }
    [ContextMenu("Å×½ºÆ®: Á×À½")]
    public void  DeadState()
    {
        BossController.Instance.ChangeState(new DeadState());
    }
}
