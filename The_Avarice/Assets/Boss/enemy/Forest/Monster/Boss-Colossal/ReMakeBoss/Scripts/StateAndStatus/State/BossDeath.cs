using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[NodeTint("#f55442")]
public class BossDeath : BaseState<BossController>
{
    [Input] public BaseState<BossController> Input;
    public string death;
    public override void Enter(BossController Data)
    {
        Data.aniController.Play(death);
    }
    public override void Excute(BossController Data)
    {
            
    }

    public override void Exit(BossController Data)
    {
        
    }
}
 