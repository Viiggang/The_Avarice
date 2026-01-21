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
        // 공격 시 X속도 멈춤
        player.ResetVelocityX();

        // 공격 시작
        player.Attack.input_Atk();
    }

    public void Exit() {}
    public void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            player.Attack.input_Atk();
        }
        else if (Input.GetKey(KeyCode.S))
        {
            stateMachine.ChangeState(player.GetSkill2State());
        }
    }
    public void LogicUpdate()
    {
        if (!player.Attack.IsAttacking())
        {
            stateMachine.ChangeState(Mathf.Abs(player.InputX) > 0.01f ? player.MoveState : player.IdleState);
        }
    }

    public void PhysicsUpdate()
    {
        if (player.IsGrounded())
        {
            player.sprite.sortingOrder = 0;
            stateMachine.ChangeState(
                Mathf.Abs(player.InputX) > 0.01f
                ? player.MoveState
                : player.IdleState
            );
        }
    }
}