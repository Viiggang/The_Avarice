using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeState : IpController
{
    private PlayerCon player;
    private Player_ControllMachine stateMachine;
    private float timer;

    private bool isOnCooldown = false;

    public ChangeState(PlayerCon player, Player_ControllMachine stateMachine)
    {
        this.player = player;
        this.stateMachine = stateMachine;
    }

    public void Enter()
    {
        if (isOnCooldown)
        {
            stateMachine.ChangeState(player.IdleState);
            return;
        }
        player.ResetVelocityX();
        timer = player.GetSkill1Duration();
        player.CanDash = false;

        if (PlayerMgr.instance.playerType.Equals(Player_Type.Ignis) && PlayerMgr.instance.Passive1 == 20)
        {
            player.Anim.SetTrigger("Passive");
       
        }
        else
        {
            player.Anim.SetTrigger("Change");
        }

        if (PlayerMgr.instance.ElementType == (Element_Type.Fire))
        {
            PlayerMgr.instance.ElementType = Element_Type.Thunder;
        }
        else if (PlayerMgr.instance.ElementType == (Element_Type.Thunder))
        {
            PlayerMgr.instance.ElementType = Element_Type.Ice;
        }
        else
        {
            PlayerMgr.instance.ElementType = Element_Type.Fire;
        }

        player.StartCoroutine(CooldownCoroutine());

        isOnCooldown = true;
    }

    public void Exit()
    {
        player.CanDash = true;
    }

    public void HandleInput() { }

    public void LogicUpdate()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            stateMachine.ChangeState(Mathf.Abs(player.InputX) > 0.01f ? player.MoveState : player.IdleState);
        }
    }

    public void PhysicsUpdate() 
    {
        float speed = player.InputX > 0 ? player.GetNormalSpeed() : -player.GetNormalSpeed();
        player.MoveHorizontally(speed);
    }

    private IEnumerator CooldownCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        isOnCooldown = false;
    }

    


}
