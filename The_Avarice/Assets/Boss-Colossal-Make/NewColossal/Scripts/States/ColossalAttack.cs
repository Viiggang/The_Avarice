using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColossalAttack : BaseState
{
    [Input] public BaseState input;
    public override void Enter()
    {
        Debug.Log("ColossalAttack_Enter");
    }
    public override void Excute()
    {
        Debug.Log("ColossalAttack_Excute");
        NodeMachine.Instance.SetNextState("Next");
    }
    public override void Exit()
    {
        Debug.Log("ColossalAttack_Exit");
    }
}
