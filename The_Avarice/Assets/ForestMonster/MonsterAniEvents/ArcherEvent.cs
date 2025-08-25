using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "ArcherAttack", menuName = "Monster/AnimationEvents/ArcherAttack")]
public class ArcherAttack :BaseAniEvent
{
    public override void Execute(List<GameObject> bulletList)
    {
        Debug.Log("Excute(List<Bullet> bulletList) Å×½ºÆ®");
        return;
        foreach (GameObject bullet in bulletList)
        {
            bool activeSelf= bullet.activeSelf;
            if (!activeSelf)
            {
                //
                bullet.gameObject.SetActive(true);
            }
        }
    }
}
[CreateAssetMenu(fileName = "ArcherNextState", menuName = "Monster/AnimationEvents/ArcherNextState")]
public class ArcherNextState : BaseAniEvent
{
    public override void Execute(MonsterController controller)
    {
        const string EndAttack = "idle";
        MonsterMachine<MonsterController> MonsterMachine = controller.MonsterMachine;
        var state = controller.State;
        MonsterMachine.ChangeState(state[EndAttack], controller);
        return;
    }
}

