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
        player.ResetVelocityX(); // 공격 시 멈춤
        player.Attack.input_Atk(); // 기존 공격 로직 호출
        player.CanMove = false;
    }

    public void Exit()
    {
        player.CanMove = true;
    }

    public void HandleInput() { }

    public void LogicUpdate()
    {
        // 애니메이션 이벤트로 상태 전환
        if (!player.Anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }

    public void PhysicsUpdate() { }
}