using System.Collections;
using UnityEngine;

public class Skill1State : IpController
{
    private PlayerCon player;
    private Player_ControllMachine stateMachine;
    private float timer;

    private bool isOnCooldown = false;

    public Skill1State(PlayerCon player, Player_ControllMachine stateMachine)
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
        player.CanMove = false;
        timer = player.GetSkill1Duration();

        if(PlayerMgr.instance.playerType == Player_Type.Paladin && PlayerMgr.instance.Passive1 == 20)
        {
            player.Anim.SetTrigger("Passive1");
            StartBuff();
        }
        else
        {
            player.Anim.SetTrigger("Skill1");
        }

        player.StartCoroutine(CooldownCoroutine());

        isOnCooldown = true;
    }

    public void Exit()
    {
        
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

    public void PhysicsUpdate() { }

    private IEnumerator CooldownCoroutine()
    {
        yield return new WaitForSeconds(player.GetSkill1Cooldown());
        isOnCooldown = false;
    }

    public void StartBuff()
    {
        player.StartCoroutine(Pal_PassiveBuff());
    }

    private IEnumerator Pal_PassiveBuff()
    {
        yield return new WaitForSeconds(1);
        PlayerMgr.instance.Passive =true;
        PlayerMgr.instance.sumPassiveStack(-20);
        PlayerMgr.instance.sumPlayerHp(20f);
        PlayerMgr.instance.sumPlayerMaxHp(20f);
        PlayerMgr.instance.sumPlayerAtk(5f);
        PlayerMgr.instance.OnPassive = true;
        player.setSkill1Cooldown(0.8f);
        yield return new WaitForSeconds(6);
        PlayerMgr.instance.Passive = false;
        PlayerMgr.instance.sumPlayerHp(-20f);
        PlayerMgr.instance.sumPlayerMaxHp(-20f);
        PlayerMgr.instance.sumPlayerAtk(-5f);
        player.resetSkill1Cooldown();
        yield return new WaitForSeconds(20);
        PlayerMgr.instance.OnPassive = false;

    }
}