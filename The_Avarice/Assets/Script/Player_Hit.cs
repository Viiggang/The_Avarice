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
    public void OnHitDamage(float damage) //�ǰ� �������̽�
    {
        animator.SetTrigger("Hurt");
        Debug.Log("hurt1");
    }
}
