using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldState : IpController
{
    private readonly PlayerCon player;
    private readonly Player_ControllMachine sm;

    [SerializeField] private float parryWindow = 0.5f; 

    public ShieldState(PlayerCon player, Player_ControllMachine stateMachine)
    {
        this.player = player;
        this.sm = stateMachine;
    }

    public void Enter()
    {
        player.CanMove = false;
        player.Anim.SetTrigger("ShieldStart");
        player.Anim.SetBool("Shield", true);
        PlayerMgr.instance.Guard = 0f;
        player.Anim.SetBool("Counter", true);
        player.StartCoroutine(ParryWindowCoroutine());
    }

    public void Exit()
    {
 
    }

    public void HandleInput()
    {
        // 방패 키를 떼면 방어 종료
        if (!Input.GetKey(KeyCode.S))
        {
            PlayerMgr.instance.Guard = 1f;
            player.Anim.SetBool("Shield", false);
            player.Anim.SetTrigger("ShieldEnd");
            player.CanMove = true;
            sm.ChangeState(player.IdleState);
        }
    }

    public void LogicUpdate() { HandleInput(); }
    public void PhysicsUpdate() { }

    private IEnumerator ParryWindowCoroutine()
    {
        yield return new WaitForSeconds(parryWindow);
        player.Anim.SetBool("Counter", false);
        PlayerMgr.instance.Guard = 0.5f;
    }

 
   
}
