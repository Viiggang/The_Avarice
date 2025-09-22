using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hdsdsafdsgs : MonoBehaviour,IDamage
{
    public SpriteRenderer SpriteRenderer;
   public void OnHitDamage(float Damage)
    {
        SpriteRenderer=GetComponent<SpriteRenderer>();
        SpriteRenderer.color = Color.red;
        Invoke("sdasf", 1f);
    }
    void sdasf()
    {
        SpriteRenderer.color = Color.white;
    }
}
