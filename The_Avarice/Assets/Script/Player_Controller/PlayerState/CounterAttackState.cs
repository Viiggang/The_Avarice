using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterAttackState : IpController
{
    private readonly PlayerCon player;
    private readonly Player_ControllMachine sm;

    private float counterDuration = 0.5f;
    private float timer;

    public CounterAttackState(PlayerCon player, Player_ControllMachine sm)
    {
        this.player = player;
        this.sm = sm;
    }

    public void Enter()
    {
        player.CanMove = false;
        timer = counterDuration;
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
            // ���� Ű�� ��� ������ ������ ��� ����ڼ� ����
            if (Input.GetKey(KeyCode.S))
                sm.ChangeState(player.Skill2State);
            else
                sm.ChangeState(player.IdleState);
        }
    }

    public void PhysicsUpdate() { }
}
