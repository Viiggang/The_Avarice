using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "patrol", menuName = "ArcherStates/Patrol")]
public class ArcherPatrol : MonsterStates<MonsterController>
{
    private Transform findPlayer;
    private float movespeed;
    
    private MonsterController manager;
    [Leein.InspectorName("실행할 애니메이션 이름")][SerializeField]private string PatrolPlay;

 
    public override void Enter(MonsterController manager)
    {
        Debug.Log("ArcherPatrol_Enter");
        this.manager = manager;
      
    }
    public override void Update()
    {

    }
    public override void Exit()
    {

    }
}