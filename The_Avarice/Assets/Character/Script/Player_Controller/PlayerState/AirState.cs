using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirState : IpController
{
    private PlayerCon player;
    private Player_ControllMachine stateMachine;

    private bool jumpStarted; // 점프 시작인지, 낙하 시작인지 구분

    public AirState(PlayerCon player, Player_ControllMachine stateMachine)
    {
        this.player = player;
        this.stateMachine = stateMachine;
    }

    public void Enter()
    {
        player.sprite.sortingOrder = 99;
        player.Anim.SetBool("isJump", true);
        jumpStarted = player.Rigid.velocity.y > 0.01f; // 양수면 점프, 아니면 낙하
    }

    public void Exit()
    {
    }

    public void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && player.CanDash)
            stateMachine.ChangeState(player.DashState);
        else if (Input.GetKeyDown(KeyCode.C))
            stateMachine.ChangeState(player.AttackState);
    }

    public void LogicUpdate()
    {
        // 공중 이동
        if (Mathf.Abs(player.InputX) > 0.01f)
        {
            player.MoveHorizontally(player.InputX * player.GetNormalSpeed());
            player.SetDirection(player.InputX);
        }

        // 착지 시 Idle 또는 Move로 전환
        if (player.IsGrounded())
        {
            player.sprite.sortingOrder = 0;
            stateMachine.ChangeState(Mathf.Abs(player.InputX) > 0.01f ? player.MoveState : player.IdleState);
        }
    }

    public void PhysicsUpdate()
    {
        // 점프 도중 하강 전환 확인 → 필요 시 애니메이션 전환 가능
    }
}