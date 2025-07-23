using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class WakeState :IBossState
{
    public float moveSpeed = 2f;     // 보스 이동 속도
    public float stopDistance = 3f;
    public void Enter(BossController boss)
    {
        boss.animator.SetTrigger("Wake");
    }
    public void Execute(BossController boss)
    {
        float distanceX = Mathf.Abs(boss.transform.position.x - boss.target.position.x);
        if (distanceX > stopDistance)//거리가 멀면
        {
           
            boss.ChangeState(new chaseState());// 추적을 하기 위해 추적상태로 변경
        }
        else //거리가 가까우면
        {
            boss.ChangeState(new AttackState());
        }
 
    }
    public void Exit(BossController boss)
    {

    }

    
}
