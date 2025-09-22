using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(
    fileName = "Attack",     // ������ ���� �⺻ �̸�
    menuName = "Monster/StateData/Attack" // �޴� ���

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
