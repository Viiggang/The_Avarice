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
        public float moveSpeed = 2f;     // ���� �̵� �ӵ�
        public float stopDistance = 3f;


        private float delaytime = 1.5f;
        public void Enter(BossController boss)
        {

            boss.animator.SetTrigger("Wake");

        }
        public void Execute(BossController boss)
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
        public void Exit(BossController boss)
        {

        }
        private IEnumerator NextDelay()
        {
            // �ִϸ��̼� ���̸�ŭ ��� (��: 1��)
            yield return new WaitForSeconds(delaytime);
        }

    }
}
