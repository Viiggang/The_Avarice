using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdle : BaseState<BossController>
{
    [Input] public BaseState<BossController> Input;
    [Output] public BaseState<BossController> Chase;
    [Output] public BaseState<BossController> death;
     
    public override void Enter(BossController Data)
    {
        Data.stateMachine.SetNextState("Chase");
    }
    public override void Excute(BossController Data)
    {
       
    }
    public override void Exit(BossController Data) 
    {

    }
}
