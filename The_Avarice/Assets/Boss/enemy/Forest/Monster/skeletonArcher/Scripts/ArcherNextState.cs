using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ArcherNextState", menuName = "Monster/AnimationEvents/ArcherNextState")]
public class ArcherNextState : BaseAniEvent
{
    public override void Execute(MonsterController controller = null, params object[] data)
    {
     
        const string NextState = "idle";
        MonsterMachine<MonsterController> MonsterMachine = controller.MonsterMachine;
        var state = controller.State;
        MonsterMachine.ChangeState(state[NextState], controller);

    }
}
