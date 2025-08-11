using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : IpController
{
    private PlayerCon player;
    private Player_ControllMachine stateMachine;

    public MoveState(PlayerCon player, Player_ControllMachine stateMachine)
    {
        this.player = player;
        this.stateMachine = stateMachine;
    }

    public void Enter()
    {
        player.Anim.SetBool("isJump", false);
        player.Anim.SetBool("isMove", true);
        player.CanMove = true;
    }

    public void Exit()
    {
        player.Anim.SetBool("isMove", false);
    }

    public void HandleInput()
    {

        // 이동 입력 없으면 Idle
        if (Mathf.Abs(player.InputX) < 0.01f)
        {
            stateMachine.ChangeState(player.IdleState);
        }
        else if (player.JumpInput)
        {
            stateMachine.ChangeState(player.JumpState);
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift) && player.CanDash)
        {
            stateMachine.ChangeState(player.DashState);
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            stateMachine.ChangeState(player.AttackState);
        }
    }

    public void LogicUpdate()
    {
        if (!player.IsGrounded())
        {
            stateMachine.ChangeState(player.AirState);
            return;
        }
        // 방향 전환
        player.SetDirection(player.InputX);
    }

    public void PhysicsUpdate() 
    {
        if (player.CanMove == false)
        {
            player.Rigid.velocity = new Vector2(0, player.Rigid.velocity.y);

        }
        else
        {
            float speed = player.InputX > 0 ? player.GetNormalSpeed() : -player.GetNormalSpeed();
            player.MoveHorizontally(speed);
        }
       
    }

}