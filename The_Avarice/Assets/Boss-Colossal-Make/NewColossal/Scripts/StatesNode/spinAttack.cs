using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spinAttackNode : BaseState
{
    public Sprite[] sprites;
    [Input] public BaseState input;
    public override void Enter()
    {
        Debug.Log("spinAttackNode¡¯¿‘");
        ColossalHandler.Instance.animanager.Actions[1].Excute();
    }
    public override void Excute()
    {
        NodeMachine.Instance.SetNextState("Next");
    }
    public override void Exit()
    {

    }
}
