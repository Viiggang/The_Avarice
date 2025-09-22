using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ColossalEvent;

public class SlamDown : BaseState<BossController>
{
   
    [Input] public BaseState<BossController> Input;
    
    public string attack;
    public override void Enter(BossController Data)
    {
        MonsterAniController ani = Data.aniController;
        ani.Play(attack);
     
    }
    public override void Excute(BossController Data)
    {

    }
    public override void Exit(BossController Data)
    {

    }
}
