using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pattern1 :BaseState
{
    [Input] public BaseState input;
    [Output] public BaseState Attack1;
    [Output] public BaseState Attack2;
    [Output] public BaseState Attack3;
    int value;
    public override void Enter()
    {
         value = Random.Range(1, 4);
        Debug.Log($"pattern1:{value}");
        SetAttackpattern();
        switch (value)
        {
            case 1:
                NodeMachine.Instance.SetNextState("Attack1");
                break;
            case 2:
                NodeMachine.Instance.SetNextState("Attack2");
                break;
            case 3:
                NodeMachine.Instance.SetNextState("Attack3");
                break;
        }
    }
    public override void Excute()
    {
       

    }

    public override void Exit()
    {

    }
    void SetAttackpattern()
    {
        ColossalHandler.Instance.animanager.Page1();
    }
}
