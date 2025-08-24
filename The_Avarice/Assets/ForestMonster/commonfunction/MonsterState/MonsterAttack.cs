using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(
    fileName = "Attack",     // 생성될 에셋 기본 이름
    menuName = "Monster/StateData/Attack" // 메뉴 경로

)]
public class MonsterAttack : MonsterStates
{
    public override void Enter(MonsterController controller)
    {
        controller.StartState = "attack";
        controller.aniManager.Play("attack");
    }
    public override void Excute(MonsterController controller)
    {
     
    }
    public override void Exit(MonsterController controller)
    {

    }
}
