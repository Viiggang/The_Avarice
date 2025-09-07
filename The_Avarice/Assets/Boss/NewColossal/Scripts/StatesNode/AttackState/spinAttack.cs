using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spinAttackNode : BaseState
{
    public Sprite[] sprites;
    [Input] public BaseState input;
    private AniManager Manager;
    public override void Enter()
    {
        Debug.Log("spinAttackNode¡¯¿‘");
        Managereference();
        Attack();

    }
    public override void Excute()
    {
       // NodeMachine.Instance.SetNextState("Next");
    }
    public override void Exit()
    {

    }
    private void Managereference()
    {
        if (Manager == null)
        {
            Manager = ColossalHandler.Instance.animanager;
        }

    }
    private void Attack()
    {
        Manager.Actions["spin"].Excute();
    }
}
