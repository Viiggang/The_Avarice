using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;

public class CounterAttackState : IpController
{
    private readonly PlayerCon player;
    private readonly Player_ControllMachine sm;

    private float counterDuration = 0.5f;
    private float timer;
    IpController skillState;
    public CounterAttackState(PlayerCon player, Player_ControllMachine sm)
    {
        this.player = player;
        this.sm = sm;
    }

    public void Enter()
    {
        player.CanMove = false;
        timer = counterDuration;
        skillState = player.GetSkill2State();
        
        Debug.Log("Counter");
        player.Anim.SetTrigger("Counter");  
    }

    public void Exit()
    {
        player.CanMove = true;
    }
    public void HandleInput() { }

    public void LogicUpdate()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            // 방패 키를 계속 누르고 있으면 즉시 방어자세 복귀
            if (Input.GetKey(KeyCode.S))
                   player.ControlMachine.ChangeState(skillState);
                
                else
                sm.ChangeState(player.IdleState);
        }
    }

    public void PhysicsUpdate() { }
}
