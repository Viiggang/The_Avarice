using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "patrol", menuName = "ArcherStates/Patrol")]
public class ArcherPatrol : MonsterStates<MonsterManager>
{
    private Transform findPlayer;
    private float movespeed;
    
    private MonsterManager manager;
    [Leein.InspectorName("실행할 애니메이션 이름")][SerializeField]private string PatrolPlay;

 
    public override void Enter(MonsterManager manager)
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