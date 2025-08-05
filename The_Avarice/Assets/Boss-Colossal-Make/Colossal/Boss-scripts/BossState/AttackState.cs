using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.PlayerSettings;
using static UnityEngine.RuleTile.TilingRuleOutput;
namespace Boss_Colossal
{
    public class AttackState : IBossState
    {
        
        public void Enter(BossController boss)
        {

            boss.animator.SetTrigger("SuperAttack");

        }
        public void Execute(BossController boss)
        {

        }
        public void Exit(BossController boss)
        {

        }

    }
}
