using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
namespace Boss_Colossal
{
    public class IdleState : IBossState
    {
        public readonly float stopDistance = 3f;

        private const float delayTime=1.0f;
        private const float InitWaitTime = 0.0f;
        private float wait = 0.0f;

        public void Enter(BossController boss)
        {
            Debug.Log("IdleState_Enter");
            boss.animator.SetTrigger("Idle");

        }
        public void Execute(BossController boss)
        {
                Think(boss);
        }

        public void Exit(BossController boss)
        {
            Debug.Log("IdleState_Exit");
            wait = InitWaitTime;

        }

       public void Think(BossController boss)
       {
             
            if (IsPlayerFar(boss))
            {
                boss.ChangeState(new chaseState());
            }
            else
            {
                DecideAttackPattern(boss);
            }
       }
      
       private bool IsPlayerFar(BossController boss)
       {
            
            float distanceX = Mathf.Abs(boss.transform.position.x - boss.target.position.x);
            return (distanceX > stopDistance);
       }
        private void DecideAttackPattern(BossController boss)
        {
            float HalfDivide = 2;
            float BosshalfHp = boss.BossAblity.MaxHp / HalfDivide; // MaxHp 기준 사용 추천

            if (boss.BossAblity.Hp < BosshalfHp)
            {
                // 체력 절반 이하 → 슈퍼 공격
                boss.ChangeState(new AttackState());
            }
            else
            {
                NormalAttack(boss);
            }
        }

        private  void NormalAttack(BossController boss)
        {
            int AttackPatternMax = 3;
            int AttackPatternMin = 1;
            // 체력 절반 이상 → 랜덤 일반 공격
            int AttackPattern = UnityEngine.Random.Range(AttackPatternMin, AttackPatternMax); // 1 또는 2

            switch (AttackPattern)
            {
                case 1:
                    boss.ChangeState(new MeleeAttack());
                    break;
                case 2:
                    boss.ChangeState(new RangeAttack());
                    break;
            }
        }
    }
}
