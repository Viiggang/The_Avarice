using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testplayer : MonoBehaviour,IDamage
{
    public void OnHitDamage(float Damage)
    {
        Debug.Log("Hit");
    }// 피격구현
}
