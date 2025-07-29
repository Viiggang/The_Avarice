using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Boss_Colossal
{
    public class chaseState : IBossState
    {
        public float moveSpeed = 2f;     // 보스 이동 속도
        public float stopDistance = 3f;

        private float delayTime;
        private float elapsedTime;
        public void Enter(BossController boss)
        {
            delayTime = 1.5f;
            elapsedTime = 0.0f;
            boss.animator.SetTrigger("Move");
        }
        public void Execute(BossController boss)
        {
            if (elapsedTime > delayTime)
            {
                Chase(boss);
            }
            else
            {
                elapsedTime += Time.deltaTime;
            }

        }
        public void Exit(BossController boss)
        {

        }
        public void Chase(BossController boss)
        {
            float distanceX = Mathf.Abs(boss.Boss_Pos.transform.position.x - boss.target.position.x);
            if (distanceX > stopDistance)//거리가 멀면
            {
                Vector2 direction = (boss.target.position - boss.Boss_Pos.transform.position).normalized;
                boss.Boss_Pos.transform.position += new Vector3(direction.x, 0f, 0f) * moveSpeed * Time.deltaTime;
            }
            else //근거리면
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
