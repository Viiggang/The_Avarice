using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Hit : MonoBehaviour, IDamage
{
    Collider2D collider2D;
    public Animator animator;

    private void OnEnable()
    {
        collider2D = GetComponent<Collider2D>();
        animator = GetComponentInParent<Animator>();
    }
    public void OnHitDamage(float damage) //피격 인터페이스
    {
        animator.SetTrigger("Hurt"); // 피격중 이동안되도록

    }
}
