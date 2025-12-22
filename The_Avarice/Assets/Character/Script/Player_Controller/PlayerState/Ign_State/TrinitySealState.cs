using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrinitySealState : IpController
{
    private readonly PlayerCon player;
    private readonly Player_ControllMachine stateMachine;
    private float timer;
    private bool FireCoolDawn = false;
    private bool ThunderCoolDawn = false;
    private bool IceCoolDawn = false;

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


        switch (PlayerMgr.instance.ElementType)
        {
            case Element_Type.Fire:
                if (FireCoolDawn == false)
                    player.Anim.SetTrigger("Fire");
                FireCoolDawn = true;
                player.StartCoroutine(FireCooldownCoroutine());
                break;
            case Element_Type.Thunder:
                if (ThunderCoolDawn == false)
                    player.Anim.SetTrigger("Thunder");
                ThunderCoolDawn= true;
                player.StartCoroutine(ThunderCooldownCoroutine());
                break;
            case Element_Type.Ice:
                if (IceCoolDawn == false)
                    player.Anim.SetTrigger("Ice");
                IceCoolDawn = true;
                player.StartCoroutine(IceCooldownCoroutine());
                break;

        }




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


    private IEnumerator FireCooldownCoroutine()
    {
        yield return new WaitForSeconds(player.GetSkill1Cooldown());
        FireCoolDawn = false;
    }
    private IEnumerator ThunderCooldownCoroutine()
    {
        yield return new WaitForSeconds(player.GetSkill1Cooldown());
        ThunderCoolDawn = false;
    }
    private IEnumerator IceCooldownCoroutine()
    {
        yield return new WaitForSeconds(player.GetSkill1Cooldown());
        IceCoolDawn = false;
    }

}
