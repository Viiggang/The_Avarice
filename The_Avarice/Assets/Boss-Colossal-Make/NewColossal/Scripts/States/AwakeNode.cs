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
        GetValueFromStartNode(); // 여기서 호출
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
    public void GetValueFromStartNode()
    {
        NodePort port = GetInputPort("Input");
        NodePort port2 = GetOutputPort("sd");
        var value = GetInputValue<StartNode>("Input");
        if ((value is null))
        {
            
        }
        else
        {
            Debug.Log($"dddddddddddddddd{value.name}");
           
        }
        if (port != null)
        {
            Debug.Log("StartNode와 연결 ");
            Node connectedNode = port.Connection.node;
            var start = port.Connection.node as StartNode;
          
        }
        else
        {
            Debug.Log("StartNode와 연결되지 않았습니다.");
        }
    }
}
