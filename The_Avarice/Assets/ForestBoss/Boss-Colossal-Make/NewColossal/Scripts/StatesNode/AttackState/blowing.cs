using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateNodeMenu("Colossal/blowing")]
public class blowing :BaseState
{
    [Input] public BaseState input;
    public Sprite[] sprites;
    private AniManager Manager;
    public override void Enter()
    {
        Debug.Log("blowing¡¯¿‘");
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
        Manager = ColossalHandler.Instance.animanager;
    }
    private void Attack()
    {
        Manager.Actions["blowing"].Excute();
    }
}
