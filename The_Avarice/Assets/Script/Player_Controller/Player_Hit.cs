using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Hit : MonoBehaviour, IDamage
{
    Collider2D collider2D;
    public Animator animator;
    public PlayerCon player;

    private void OnEnable()
    {
        collider2D = GetComponent<Collider2D>();
        animator = GetComponentInParent<Animator>();
        player = GetComponentInParent<PlayerCon>();
    }
    public void OnHitDamage(float damage) 
    {
        animator.SetTrigger("Hurt");
        player.CanMove = false;
    }
}
