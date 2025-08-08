using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slamDown : BaseState
{
    [Input] public BaseState input;
    public Sprite[] sprites;
    public override void Enter()
    {
        Debug.Log("slamDown¡¯¿‘");
        ColossalHandler.Instance.animanager.Actions[2].Excute();
    }
    public override void Excute()
    {
        NodeMachine.Instance.SetNextState("Next");
    }

    public override void Exit()
    {

    }
}
