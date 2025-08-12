using System.Collections;
using UnityEngine;

public class Skill1State : IpController
{
    private PlayerCon player;
    private Player_ControllMachine stateMachine;

   /* private float skillDuration = 1.0f;      // 스킬 지속시간 (원하는 값으로 조정)
    private float skillCooldown = 3.0f; */     // 쿨타임
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
            // 쿨타임 중이면 Idle로 복귀
            stateMachine.ChangeState(player.IdleState);
            return;
        }
        player.ResetVelocityX();
        player.CanMove = false;
        timer = player.GetSkill1Duration();

        player.Anim.SetTrigger("Skill1");

        // 쿨타임 코루틴 시작
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
            // 스킬 종료 후 상태 전환
            stateMachine.ChangeState(Mathf.Abs(player.InputX) > 0.01f ? player.MoveState : player.IdleState);
        }
    }

    public void PhysicsUpdate() { }

    private IEnumerator CooldownCoroutine()
    {
        yield return new WaitForSeconds(player.GetSkill1Cooldown());
        isOnCooldown = false;
    }
}