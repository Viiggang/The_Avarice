using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class purgeblow : BaseState<BossController>
{

    [Input] public BaseState<BossController> Input;
    
    public string attack;
    public override void Enter(BossController Data)
    {
        MonsterAniController ani = Data.aniController;
        ani.Play(attack);
        Data.BossSkillGroup.OnCoolTime();
      
    }
    public override void Excute(BossController Data)
    {

    }
    public override void Exit(BossController Data)
    {

    }
}