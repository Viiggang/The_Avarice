using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Boss_Colossal
{
    public class IdleState : IBossState
    {
        public readonly float stopDistance = 3f;
        public void Enter(BossController boss)
        {
            boss.animator.SetTrigger("Idle");

        }
        public void Execute(BossController boss)
        {
            Think(boss);
        }

        public void Exit(BossController boss)
        {


        }
        public void Think(BossController boss)
        {
            float distanceX = Mathf.Abs(boss.transform.position.x - boss.target.position.x);
            if (distanceX > stopDistance)//거리가 멀면
            {
                boss.ChangeState(new chaseState());
            }
            else//거리가 가까우면 
            {
                float halfHp = (boss.BossAblity.Hp / 2);
                if (boss.BossAblity.Hp < halfHp)//반피 이하면
                {
                    boss.ChangeState(new AttackState());//반피 이하면 슈퍼 공격
                }
                else//반피 이상이면 일반 공격
                {
                    float num = UnityEngine.Random.Range(1, 3);
                    switch (num)
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

    }
}
