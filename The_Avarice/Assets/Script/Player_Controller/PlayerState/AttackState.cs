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
        // ���� �� �̵� �Ұ�
  
        // ���� �� X�ӵ� ����
        player.ResetVelocityX();

        // ���� ����
        player.Attack.input_Atk();
    }

    public void Exit()
    {
        // Exit������ ���� �̵��� Ǯ�� ���� �� Player_Atk�� EndCombo �̺�Ʈ���� Ǯ����
    }

    public void HandleInput()
    {
        // ���� �߿��� ���� Ű �Է��� Player_Atk�� ���� (�޺� ����)
        if (Input.GetKeyDown(KeyCode.C))
        {
            player.Attack.input_Atk();
        }
    }
    public void LogicUpdate()
    {
        // Player_Atk�� ���� ���� �ƴϸ� Idle/Move�� ��ȯ
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
        // ���� �߿��� ���� �ӵ� ����
        player.Rigid.velocity = new Vector2(0, player.Rigid.velocity.y);
    }
}