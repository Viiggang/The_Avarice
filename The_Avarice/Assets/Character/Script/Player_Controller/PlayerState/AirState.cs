using UnityEngine;

public class AirState : IpController
{
    private PlayerCon player;
    private Player_ControllMachine stateMachine;

    private bool jumpStarted;

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
    }

    public void Exit()
    {
        player.Anim.SetBool("isJump", false);
    }

    public void HandleInput()
    {
        // 공중에서는 ↓ + Jump 무시 (원웨이 재통과 방지)
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
        // 공중 이동
        if (Mathf.Abs(player.InputX) > 0.01f)
        {
            player.MoveHorizontally(player.InputX * player.GetNormalSpeed());
            player.SetDirection(player.InputX);
        }

        // 착지 처리
        if (player.IsGrounded())
        {
            player.sprite.sortingOrder = 0;
            stateMachine.ChangeState(
                Mathf.Abs(player.InputX) > 0.01f
                ? player.MoveState
                : player.IdleState
            );
        }
    }

    public void PhysicsUpdate()
    {
        // 필요 시 상승 → 하강 애니메이션 전환 처리 가능
    }
}