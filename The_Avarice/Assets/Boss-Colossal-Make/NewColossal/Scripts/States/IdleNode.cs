using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleNode : BaseState
{
    [Input] public BaseState input;

    [Output] public BaseState page1;
    [Output] public BaseState page2;
    [Output] public BaseState Dead;
    [Output] public BaseState Chase;
    private string Next;
    private bool flag = true; //
    public override void Enter()
    {
        Debug.Log("IdleNode eEnter");
        flag = ColossalHandler.Instance.IsBelowHalfHp();//false면 반피 이상 true면 반피 이하
        Next = (flag == true) ? "page2" : "page1";
        Debug.Log($"Next:{Next}");


    }
    public override void Excute()
    {
        Debug.Log("IdleNode Excute");
        NodeMachine.Instance.SetNextState(Next);
    }
    public override void Exit()
    {
        Debug.Log("IdleNode Exit");
    }
}
