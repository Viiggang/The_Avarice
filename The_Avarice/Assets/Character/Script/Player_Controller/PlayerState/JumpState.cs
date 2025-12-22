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
        if (player.IsOnOneWayPlatform() && Input.GetKey(KeyCode.DownArrow))
        {
            player.DisableOneWayPlatform();
            player.Rigid.velocity = new Vector2(player.Rigid.velocity.x, -2f);
            stateMachine.ChangeState(player.AirState);
            return;
        }

        player.Jump();
        stateMachine.ChangeState(player.AirState);
    }

    public void Exit() { }
    public void HandleInput() { }
    public void LogicUpdate() { }
    public void PhysicsUpdate() { }
}