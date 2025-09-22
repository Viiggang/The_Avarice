using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(
    fileName = "death",     // ������ ���� �⺻ �̸�
    menuName = "Monster/StateData/death" // �޴� ���

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
