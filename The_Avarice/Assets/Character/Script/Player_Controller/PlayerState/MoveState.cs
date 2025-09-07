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

        // �̵� �Է� ������ Idle
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
        else if (Input.GetKey(KeyCode.C))
        {
            stateMachine.ChangeState(player.AttackState);
        }
        else if(Input.GetKey(KeyCode.A))
        {
            stateMachine.ChangeState(player.Skill1State);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            stateMachine.ChangeState(player.Skill2State);
        }
    }

    public void LogicUpdate()
    {
        if (!player.IsGrounded())
        {
            stateMachine.ChangeState(player.AirState);
            return;
        }
        // ���� ��ȯ
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