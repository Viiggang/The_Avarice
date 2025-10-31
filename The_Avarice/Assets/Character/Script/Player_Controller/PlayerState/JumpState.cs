using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class JumpState : IpController
{
    private PlayerCon player;
    private Player_ControllMachine stateMachine;

    public JumpState(PlayerCon player, Player_ControllMachine stateMachine)
    {
        this.player = player;
        this.stateMachine = stateMachine;
    }

    public void Enter()
    {
        player.Jump();
        stateMachine.ChangeState(player.AirState); // 점프 시작 → 공중 상태로 전환
    }

    public void Exit() 
    {
    }
    public void HandleInput() { }
    public void LogicUpdate() { }
    public void PhysicsUpdate() { }
}