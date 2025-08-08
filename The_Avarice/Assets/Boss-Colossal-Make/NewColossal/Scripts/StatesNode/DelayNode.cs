using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayNode  : BaseState
{
    [Input] public BaseState input;
    public float DelayTime=0;
    private float ElapsedTime=0;
    private float saveTime = 0;
    [Output] public BaseState Next;
  
    public override void Enter()
    {
        Debug.Log($"µÙ∑π¿Ã Ω√∞£{DelayTime}");
        saveTime = DelayTime;
    }
    public override void Excute()
    {
        if((DelayTime- ElapsedTime)<=0)
        {
            NodeMachine.Instance.SetNextState("Next");
        }
        else
        {
            ElapsedTime += Time.deltaTime;
        }
    }
    public override void Exit()
    {
        ElapsedTime = 0;
        DelayTime = saveTime;
    }
}
