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
     
        public float stopDistance = 3f;

        private const float delayTime = 1.0f;
        private float wait = 0.0f;

        public void Enter(BossController boss)
        {

            boss.animator.SetTrigger("Wake");

        }
        public void Execute(BossController boss)
        {
            if ((delayTime - wait) < 0)
            {
                float distanceX = Mathf.Abs(boss.Boss_Pos.transform.position.x - boss.target.position.x);
                if (distanceX > stopDistance)//�Ÿ��� �ָ�
                {
                    UnityEngine.Debug.Log("WakeState-chaseState");
                    boss.ChangeState(new chaseState());// ������ �ϱ� ���� �������·� ����
                }
                else //�Ÿ��� ������
                {
                    UnityEngine.Debug.Log("WakeState-AttackState");
                    boss.ChangeState(new AttackState());
                }
            }
            else
            {
                wait += Time.deltaTime;
            }
            

        }
        public void Exit(BossController boss)
        {

        }
       

    }
}
