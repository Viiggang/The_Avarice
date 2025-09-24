using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrinitySealState : IpController
{
    private readonly PlayerCon player;
    private readonly Player_ControllMachine stateMachine;
    private float timer;


    [SerializeField] private float parryWindow = 0.5f;

    public TrinitySealState(PlayerCon player, Player_ControllMachine stateMachine)
    {
        this.player = player;
        this.stateMachine = stateMachine;
    }

    public void Enter()
    {

        if (PlayerMgr.instance.Passive.Equals(true))
        {
            stateMachine.ChangeState(player.IdleState);
            return;
        }
        player.ResetVelocityX();
        player.CanMove = false;
        timer = player.GetSkill1Duration();


        player.Anim.SetTrigger("onSpell");
        


        PlayerMgr.instance.Passive = true;
    }



    public void Exit()
    {

    }

    public void HandleInput()
    {
   
    }

    public void LogicUpdate() 
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            stateMachine.ChangeState(Mathf.Abs(player.InputX) > 0.01f ? player.MoveState : player.IdleState);
        }
    }
    public void PhysicsUpdate() { }

 


}
