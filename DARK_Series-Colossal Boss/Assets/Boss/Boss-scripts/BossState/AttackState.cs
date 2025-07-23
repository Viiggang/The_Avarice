using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class AttackState :  IBossState
{
      public Vector3 pos;
      public Vector3 size;
    public void Enter(BossController boss)
    {
       
        boss.animator.SetTrigger("SuperAttack");
        boss.ChangeState(new chaseState());
    }
    public void Execute(BossController boss)
    {
     
    }
    public void Exit(BossController boss)
    {
        
    }
   
}
