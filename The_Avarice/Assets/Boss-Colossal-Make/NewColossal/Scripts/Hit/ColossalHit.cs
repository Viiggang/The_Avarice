using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public   class ColossalHit : MonoBehaviour,IDamage
{
    // �ǰݱ���
    public void OnHitDamage(float Damage)
    {
       ColossalHandler.Instance.ablity.Hp+=Damage;
    }
}

 
