using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(
    fileName = "death",     // 생성될 에셋 기본 이름
    menuName = "Monster/StateData/death" // 메뉴 경로

)]
public class WildBoardeath :MonsterStates
{
   
    public override void Enter(MonsterController controller)
    {
        play(controller);
    }
    public override void Excute(MonsterController controller)
    {

    }
    public override void Exit(MonsterController controller)
    {

    }
    private void play(MonsterController controller)
    {
        MonsterAniController animator = controller.aniManager;
        animator.Play("death");
    }

}
