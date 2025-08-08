using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blowing :BaseState
{
    [Input] public BaseState input;
    public Sprite[] sprites;
    public override void Enter()
    {
        Debug.Log("blowing¡¯¿‘");
        ColossalHandler.Instance.animanager.Actions[3].Excute();
    }

    public override void Excute()
    {
        NodeMachine.Instance.SetNextState("Next");
    }
    public override void Exit()
    {

    }
}
