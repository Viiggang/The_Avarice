using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;
namespace Boss_Colossal
{
    public class WakeState : IBossState
    {
        public float moveSpeed = 2f;     // 보스 이동 속도
        public float stopDistance = 3f;


        private float delaytime = 1.5f;
        public void Enter(BossController boss)
        {

            boss.animator.SetTrigger("Wake");

        }
        public void Execute(BossController boss)
        {

            float distanceX = Mathf.Abs(boss.Boss_Pos.transform.position.x - boss.target.position.x);
            if (distanceX > stopDistance)//거리가 멀면
            {
                UnityEngine.Debug.Log("WakeState-chaseState");
                boss.ChangeState(new chaseState());// 추적을 하기 위해 추적상태로 변경
            }
            else //거리가 가까우면
            {
                UnityEngine.Debug.Log("WakeState-AttackState");
                boss.ChangeState(new AttackState());
            }

        }
        public void Exit(BossController boss)
        {

        }
        private IEnumerator NextDelay()
        {
            // 애니메이션 길이만큼 대기 (예: 1초)
            yield return new WaitForSeconds(delaytime);
        }

    }
}
