using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Hit : MonoBehaviour, IDamage
{
    Collider2D collider2D;
    public Animator animator;
    public PlayerCon player;
    private Player_ControllMachine stateMachine;
    public float Hp;//추가
    private void OnEnable()
    {
        collider2D = GetComponent<Collider2D>();
        animator = GetComponentInParent<Animator>();
        player = GetComponentInParent<PlayerCon>();
    }

    public void OnHitDamage(float damage)
    {
        // 널 가드
        if (player == null)
        {
            Debug.LogWarning("not [Player_Hit] PlayerCon");
            return;
        }
        PlayerMgr.instance.sumPlayerHp(damage);
        Hp = PlayerMgr.instance.getPlayerHp();//추가
        player.CanMove = false;
        // 일반 피격 로직
        if (animator != null && 1 <= PlayerMgr.instance.getPlayerHp())
        {
            animator.SetTrigger("Hurt");
        }
        else
        {
            animator.SetTrigger("Death");
        }
        


    }
}
