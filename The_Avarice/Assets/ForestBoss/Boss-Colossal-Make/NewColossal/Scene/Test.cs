using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour,IDamage
{
   public void OnHitDamage(float Damage)
    {
        Debug.Log("공격받음");
    }
}
