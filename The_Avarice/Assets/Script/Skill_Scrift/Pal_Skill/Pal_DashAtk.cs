using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pal_DashAtk : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamage damage = other.GetComponent<IDamage>(); 

        if (damage != null && other.gameObject.layer == LayerMask.NameToLayer("Enemy")) 
        {
            damage.OnHitDamage(2f);
        }

    }
}
