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
            if (distanceX > stopDistance)//�Ÿ��� �ָ�
            {
                boss.ChangeState(new chaseState());
            }
            else//�Ÿ��� ������ 
            {
                float halfHp = (boss.BossAblity.Hp / 2);
                if (boss.BossAblity.Hp < halfHp)//���� ���ϸ�
                {
                    boss.ChangeState(new AttackState());//���� ���ϸ� ���� ����
                }
                else//���� �̻��̸� �Ϲ� ����
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
