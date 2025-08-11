using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IpController
{
    private PlayerCon player;
    private Player_ControllMachine stateMachine;

    public AttackState(PlayerCon player, Player_ControllMachine stateMachine)
    {
        this.player = player;
        this.stateMachine = stateMachine;
    }

    public void Enter()
    {
        // 공격 시 이동 불가
  
        // 공격 시 X속도 멈춤
        player.ResetVelocityX();

        // 공격 시작
        player.Attack.input_Atk();
    }

    public void Exit()
    {
        // Exit에서는 굳이 이동을 풀지 않음 → Player_Atk의 EndCombo 이벤트에서 풀어줌
    }

    public void HandleInput()
    {
        // 공격 중에도 공격 키 입력은 Player_Atk로 전달 (콤보 연결)
        if (Input.GetKeyDown(KeyCode.C))
        {
            player.Attack.input_Atk();
        }
    }
    public void LogicUpdate()
    {
        // Player_Atk이 공격 중이 아니면 Idle/Move로 전환
        if (!player.Attack.IsAttacking())
        {
            stateMachine.ChangeState(Mathf.Abs(player.InputX) > 0.01f ? player.MoveState : player.IdleState);
        }
    }

    public void PhysicsUpdate()
    {
        RaycastHit2D hit = Physics2D.Raycast(player.Rigid.position, Vector2.down, 0.4f, LayerMask.GetMask("Platform"));
        if (hit)
        {
            player.Anim.SetBool("isJump", false);
        }
        // 공격 중에는 수평 속도 유지
        player.Rigid.velocity = new Vector2(0, player.Rigid.velocity.y);
    }
}