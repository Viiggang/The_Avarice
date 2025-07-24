using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : IBossState
{
    public void Enter(BossController boss)
    {
        Debug.Log("MeleeAttack");
        boss.animator.SetTrigger("MeleeAttack");
        
    }
    public void Execute(BossController boss)
    {

    }
    public void Exit(BossController boss)
    {

    }
}
