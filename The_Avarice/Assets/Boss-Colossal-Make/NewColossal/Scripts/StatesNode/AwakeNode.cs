using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XNode;

public class AwakeNode :BaseState
{
    public Sprite[] sprites;
   
    [Input] public BaseState Input;
    [Output] public BaseState next;
    
    public override void Enter()
    {
        Debug.Log("AwakeNodeEnter");
        // GetValueFromStartNode(); // 여기서 호출
     
    }
    public override void Excute()
    {
        Debug.Log("AwakeNodeExcute");
        
       // NodeMachine.Instance.SetNextState("next");
    }
    public override void Exit()
    {
        Debug.Log("AwakeNodeExit");
    }
  
     
}
