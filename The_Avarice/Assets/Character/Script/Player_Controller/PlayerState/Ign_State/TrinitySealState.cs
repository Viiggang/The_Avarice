using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrinitySealState : IpController
{
    private readonly PlayerCon player;
    private readonly Player_ControllMachine sm;

    [SerializeField] private float parryWindow = 0.5f;

    public TrinitySealState(PlayerCon player, Player_ControllMachine stateMachine)
    {
        this.player = player;
        this.sm = stateMachine;
    }

    public void Enter()
    {
        player.CanMove = false;

    }

    public void Exit()
    {

    }

    public void HandleInput()
    {
        if (!Input.GetKey(KeyCode.A))
        {

        }
    }

    public void LogicUpdate() { HandleInput(); }
    public void PhysicsUpdate() { }

 


}
