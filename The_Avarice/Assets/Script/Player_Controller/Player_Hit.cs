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
<<<<<<< Updated upstream
    public void OnHitDamage(float damage) //ÇÇ°Ý ÀÎÅÍÆäÀÌ½º
    {
        animator.SetTrigger("Hurt"); // ÇÇ°ÝÁß ÀÌµ¿¾ÈµÇµµ·Ï
=======
    public void OnHitDamage(float damage) //ï¿½Ç°ï¿½ ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½ï¿½Ì½ï¿½
    {
        animator.SetTrigger("Hurt"); // ï¿½Ç°ï¿½ï¿½ï¿½ ï¿½Ìµï¿½ï¿½ÈµÇµï¿½ï¿½ï¿½
>>>>>>> Stashed changes
        player.CanMove = false;
    }
}
