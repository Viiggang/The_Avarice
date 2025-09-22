using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Hit : MonoBehaviour, IDamage
{
    Collider2D collider2D;
    public Animator animator;
    public PlayerCon player;
    private Player_ControllMachine stateMachine;
    public float Hp;//�߰�
    private void OnEnable()
    {
        collider2D = GetComponent<Collider2D>();
        animator = GetComponentInParent<Animator>();
        player = GetComponentInParent<PlayerCon>();
    }

    public void OnHitDamage(float damage)
    {
        // �� ����
        if (player == null)
        {
            Debug.LogWarning("not [Player_Hit] PlayerCon");
            return;
        }
        PlayerMgr.instance.sumPlayerHp(damage);
        Hp = PlayerMgr.instance.getPlayerHp();//�߰�
        player.CanMove = false;
        // �Ϲ� �ǰ� ����
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
