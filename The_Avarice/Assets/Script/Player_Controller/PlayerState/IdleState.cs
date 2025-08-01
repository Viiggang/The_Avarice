using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : IpController
{
    private PlayerCon player;
    private Player_ControllMachine stateMachine;

    public IdleState(PlayerCon player, Player_ControllMachine stateMachine)
    {
        this.player = player;
        this.stateMachine = stateMachine;
    }

    public void Enter()
    {
        player.Anim.SetBool("isMove", false);
    }

    public void Exit() { }

    public void HandleInput()
    {
      if (Mathf.Abs(player.InputX) > 0.01f)
        {
            stateMachine.ChangeState(player.MoveState);
        }
        else if (player.JumpInput)
        {
            stateMachine.ChangeState(player.JumpState);
        }
        else if (Input.GetKeyDown(KeyCode.LeftShift) && player.CanDash && Mathf.Abs(player.InputX) > 0.01f)
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
        // Idle������ ���� �ӵ� 0 ����
        player.MoveHorizontally(0);
    }

    public void PhysicsUpdate() { }
}
