using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using XNode;

public class StartNode : BaseState
{

    [Output] public BaseState next;
    public override void Enter() 
    {
     
    }
    public override void Excute()
    {
        Debug.Log("StartNodeExcute");
       
      NodeMachine.Instance.SetNextState("next");
    }
    public override void Exit()
    {
        NodeMachine.Instance.Invoke("ssd", 0.1f);
    }
    
}