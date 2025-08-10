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
        player.Anim.SetBool("isMove", true);
    }

    public void Exit()
    {
        player.Anim.SetBool("isMove", false);
    }

    public void HandleInput()
    {
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
        // 이동 처리
        float speed = player.InputX > 0 ? player.GetNormalSpeed() : -player.GetNormalSpeed();
        player.MoveHorizontally(speed);
        player.SetDirection(player.InputX);
    }

    public void PhysicsUpdate() { }
}