using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AwakeNode :BaseState
{
    [Input] public BaseState Input;
    [Output] public BaseState next;
   
    public override void Enter()
    {
        Debug.Log("AwakeNodeEnter");
    }
    public override void Excute()
    {
        Debug.Log("AwakeNodeExcute");
        NodeMachine.Instance.SetNextState("next");
    }
    public override void Exit()
    {
        Debug.Log("AwakeNodeExit");
    }
}
