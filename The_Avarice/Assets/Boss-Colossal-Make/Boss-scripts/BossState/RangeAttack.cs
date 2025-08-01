using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : IBossState
{
    public void Enter(BossController boss)
    {
        Debug.Log("RangeAttack");
        boss.animator.SetTrigger("RangeAttack");
         

    }
    public void Execute(BossController boss)
    {

    }
    public void Exit(BossController boss)
    {

    }
}

