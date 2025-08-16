using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Chase", menuName = "ArcherStates/Chase")]
public class ArcherChase : MonsterStates<MonsterManager>
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