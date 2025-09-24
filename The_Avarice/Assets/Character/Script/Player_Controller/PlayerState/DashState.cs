using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : IpController
{
    private PlayerCon player;
    private Player_ControllMachine stateMachine;
    private float dashTimer;
    private Vector2 dashDirection;

    public DashState(PlayerCon player, Player_ControllMachine stateMachine)
    {
        this.player = player;
        this.stateMachine = stateMachine;
    }

    public void Enter()
    {
        player.IsDashing = true;
        player.CanDash = false;
        player.CanMove = false;

        dashDirection = PlayerMgr.instance.Direction ? Vector2.right : Vector2.left;
        player.Anim.SetTrigger("Dash");
        player.EnableHitBox(false);
        if (PlayerMgr.instance.playerType == Player_Type.Paladin)
            player.EnableExtraHitBox1();

        player.Rigid.gravityScale = 0f;
        player.Rigid.velocity = dashDirection * player.GetDashSpeed();

        dashTimer = player.GetDashDuration();
    }

    public void Exit()
    {
        player.IsDashing = false;
        player.CanMove = true;
        player.Rigid.gravityScale = 2f;
        player.Rigid.velocity = Vector2.zero;
        player.EnableHitBox(true);

        if (PlayerMgr.instance.playerType == Player_Type.Paladin)
            player.EnableExtraHitBox1();

        // Äð´Ù¿î ½ÃÀÛ
        player.StartCoroutine(DashCooldown());
    }

    public void HandleInput() { }

    public void LogicUpdate()
    {
        dashTimer -= Time.deltaTime;
        if (dashTimer <= 0)
        {
            stateMachine.ChangeState(player.IdleState);
        }
    }

    public void PhysicsUpdate() { }

    private IEnumerator DashCooldown()
    {
        yield return new WaitForSeconds(player.GetDashCooldown());
        player.CanDash = true;
    }
}