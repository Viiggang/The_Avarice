using UnityEngine;

public class AirState : IpController
{
    private PlayerCon player;
    private Player_ControllMachine stateMachine;

    private bool jumpStarted;
    private bool DoubleJump;

    private float m_inputX;

    public AirState(PlayerCon player, Player_ControllMachine stateMachine)
    {
        this.player = player;
        this.stateMachine = stateMachine;
    }

    public void Enter()
    {
        player.sprite.sortingOrder = 99;
        player.Anim.SetBool("isJump", true);
        jumpStarted = player.Rigid.velocity.y > 0.01f;
        DoubleJump = true;
        m_inputX = Mathf.Sign(player.InputX);
    }

    public void Exit()
    {
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

        if (Input.GetKeyDown(KeyCode.Space) && DoubleJump == true)
        {
            player.Anim.SetTrigger("jump2");
            player.Jump2();
            DoubleJump = false;
        }
    }

    public void LogicUpdate()
    {
        // 공중 이동
        if (Mathf.Abs(player.InputX) > 0.01f && player.Rigid.velocity.y != 0)
        {
            float checkinput = Mathf.Sign(player.InputX);
            if (m_inputX != player.InputX)
            {
                player.MoveHorizontally(player.InputX * player.GetNormalSpeed() * 0.5f);
                player.SetDirection(player.InputX);
            }
            else
            {
                player.MoveHorizontally(player.InputX * player.GetNormalSpeed());
                player.SetDirection(player.InputX);
            }
        }

        // 착지 처리
        if (player.IsGrounded())
        {
            player.sprite.sortingOrder = 0;
            player.CheckGround();
            stateMachine.ChangeState(
                Mathf.Abs(player.InputX) > 0.01f
                ? player.MoveState
                : player.IdleState
            );
        }
    }

    public void PhysicsUpdate()
    {
    }
}