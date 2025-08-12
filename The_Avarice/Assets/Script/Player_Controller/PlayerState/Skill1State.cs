using System.Collections;
using UnityEngine;

public class Skill1State : IpController
{
    private PlayerCon player;
    private Player_ControllMachine stateMachine;

   /* private float skillDuration = 1.0f;      // ��ų ���ӽð� (���ϴ� ������ ����)
    private float skillCooldown = 3.0f; */     // ��Ÿ��
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
            // ��Ÿ�� ���̸� Idle�� ����
            stateMachine.ChangeState(player.IdleState);
            return;
        }
        player.ResetVelocityX();
        player.CanMove = false;
        timer = player.GetSkill1Duration();

        player.Anim.SetTrigger("Skill1");

        // ��Ÿ�� �ڷ�ƾ ����
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
            // ��ų ���� �� ���� ��ȯ
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