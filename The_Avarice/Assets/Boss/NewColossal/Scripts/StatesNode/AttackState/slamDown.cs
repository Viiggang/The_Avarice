using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slamDown : BaseState
{
    [Input] public BaseState input;
    public Sprite[] sprites;
    private AniManager Manager;
    public override void Enter()
    {
        Debug.Log("slamDown¡¯¿‘");
        Managereference();
        Attack();
    }
    public override void Excute()
    {
        //NodeMachine.Instance.SetNextState("Next");
    }

    public override void Exit()
    {

    }
    private void Managereference()
    {
        if(Manager == null)
        {
            Manager = ColossalHandler.Instance.animanager;
        }
      
    }
    private void Attack()
    {
        Manager.Actions["slamDown"].Excute();
    }
}
