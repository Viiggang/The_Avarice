using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "attack",menuName = "ArcherStates/Attack")]
public class ArcherAttack : MonsterStates<MonsterController>
{
    private MonsterController manager;
    [SerializeField] private string attack;
    public override void Enter(MonsterController manager)
    {
        manager.aniManager.Play(attack);
    }
    public override void Update()
    {

    }
    public override void Exit()
    {

    }
}
