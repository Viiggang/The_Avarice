using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "death", menuName = "ArcherStates/death")]
public class Archerdeath : MonsterStates<MonsterController>
{
    private MonsterController manager;
    public override void Enter(MonsterController manager)
    {
        this.manager = manager;
    }
    public override void Update()
    {

    }
    public override void Exit()
    {

    }
}
