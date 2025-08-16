using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "attack",menuName = "ArcherStates/Attack")]
public class ArcherAttack : MonsterStates<MonsterManager>
{
    private MonsterManager manager;
    public override void Enter(MonsterManager manager)
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
