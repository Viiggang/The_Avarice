using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateNodeMenu("Colossal/idle")]
public class IdleNode : BaseState
{
    [Input] public BaseState input;

    [Output] public BaseState page1;
    [Output] public BaseState page2;
    [Output] public BaseState Dead;
    [Output] public BaseState Chase;
    public Sprite[] sprites;
    private string Next;
    private bool IsHalfHp = true; //
    private bool IsDead = false;
    private bool IsNear = false;

 
    public override void Enter()
    {
        Debug.Log("IdleNode eEnter");
        IsDead   = ColossalHandler.Instance.IsDead();//�׾����� Ȯ��
        IsNear   = ColossalHandler.Instance.IsNear(); //�÷��̰� �������� Ȯ��
        IsHalfHp = ColossalHandler.Instance.IsBelowHalfHp();//false�� ���� �̻� true�� ���� ����

        Next = (IsDead == true)  ? "Dead" :
               (IsNear == false) ? "Chase":
               (IsHalfHp == true)? "page2":
                                   "page1";

        Debug.Log($"Next:{Next}");
        NodeMachine.Instance.SetNextState(Next);

    }
    public override void Excute()
    {
        Debug.Log("IdleNode Excute");
        
    }
    public override void Exit()
    {
        Debug.Log("IdleNode Exit");
    }

    
}
