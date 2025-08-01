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
        player.ResetVelocityX(); // ���� �� ����
        player.Attack.input_Atk(); // ���� ���� ���� ȣ��
        player.CanMove = false;
    }

    public void Exit()
    {
        player.CanMove = true;
    }

    public void HandleInput() { }

    public void LogicUpdate()
    {
        // �ִϸ��̼� �̺�Ʈ�� ���� ��ȯ
        if (!player.Anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }

    public void PhysicsUpdate() { }
}