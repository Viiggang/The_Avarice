using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class DeadState :IBossState
{
    public void Enter(BossController boss)
    {
        boss.animator.SetTrigger("Dead");
         
        

    }
    public void Execute(BossController boss)
    {

    }
    public void Exit(BossController boss)
    {

    }
}
