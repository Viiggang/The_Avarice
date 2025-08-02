using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpState : IpController
{
    private PlayerCon player;
    private Player_ControllMachine stateMachine;

    public JumpState(PlayerCon player, Player_ControllMachine stateMachine)
    {
        this.player = player;
        this.stateMachine = stateMachine;
    }

    public void Enter()
    {
        player.Anim.SetBool("isJump", true);
        player.Jump();
    }

    public void Exit()
    {
        player.Anim.SetBool("isJump", false);
    }

    public void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && player.CanDash)
        {
            stateMachine.ChangeState(player.DashState);
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            stateMachine.ChangeState(player.AttackState);
        }
    }

    public void LogicUpdate()
    {
        // °øÁß¿¡¼­ ÁÂ¿ì ÀÌµ¿
        float speed = player.InputX > 0 ? player.GetNormalSpeed() : -player.GetNormalSpeed();
        if (Mathf.Abs(player.InputX) > 0.01f)
        {
            player.MoveHorizontally(speed);
            player.SetDirection(player.InputX);
        }

        // ¶¥¿¡ ´êÀ¸¸é Idle ÀüÈ¯
        RaycastHit2D hit = Physics2D.Raycast(player.Rigid.position, Vector2.down, 0.5f, LayerMask.GetMask("Platform"));
        if (hit.collider != null)
        {
            stateMachine.ChangeState(Mathf.Abs(player.InputX) > 0.01f ? player.MoveState : player.IdleState);
        }
    }

    public void PhysicsUpdate() { }
}