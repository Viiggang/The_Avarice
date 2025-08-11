using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public abstract class BaseState : Node
{
   
    // Use this for initialization
    protected override void Init()
    {
		base.Init();
		
	}

	public override object GetValue(NodePort port)
	{
        if (port.fieldName == "inputValue")
        {
            // 연결된 노드에서 값을 가져옴
            int value = GetInputValue<int>("inputValue");
            return value;
        }
        return null;
    }
  
	public abstract void Enter();
    public abstract void Excute();
    public abstract void Exit();
}