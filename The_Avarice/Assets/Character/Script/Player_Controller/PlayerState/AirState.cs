using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirState : IpController
{
    private PlayerCon player;
    private Player_ControllMachine stateMachine;

    private bool jumpStarted; // ���� ��������, ���� �������� ����

    public AirState(PlayerCon player, Player_ControllMachine stateMachine)
    {
        this.player = player;
        this.stateMachine = stateMachine;
    }

    public void Enter()
    {
        player.sprite.sortingOrder = 99;
        player.Anim.SetBool("isJump", true);
        jumpStarted = player.Rigid.velocity.y > 0.01f; // ����� ����, �ƴϸ� ����
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
        // ���� �̵�
        if (Mathf.Abs(player.InputX) > 0.01f)
        {
            player.MoveHorizontally(player.InputX * player.GetNormalSpeed());
            player.SetDirection(player.InputX);
        }

        // ���� �� Idle �Ǵ� Move�� ��ȯ
        if (player.IsGrounded())
        {
            player.sprite.sortingOrder = 0;
            stateMachine.ChangeState(Mathf.Abs(player.InputX) > 0.01f ? player.MoveState : player.IdleState);
        }
    }

    public void PhysicsUpdate()
    {
        // ���� ���� �ϰ� ��ȯ Ȯ�� �� �ʿ� �� �ִϸ��̼� ��ȯ ����
    }
}