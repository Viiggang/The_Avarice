using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using XNode;

[CreateNodeMenu("Colossal/Start")]
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
        Debug.Log("StartNodeExit");
    }
    //public override object GetValue(NodePort port)
    //{
    //    if (port.fieldName == "next")
    //    {
    //        Debug.Log($"StartNode->{port.fieldName}");
    //        return this; // 자기 자신 반환
    //    }
    //    Debug.Log($"StartNode->{port.fieldName}");
    //    return null;
    //}
}