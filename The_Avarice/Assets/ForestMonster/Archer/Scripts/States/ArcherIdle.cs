using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "idle", menuName = "ArcherStates/idle")]
public class ArcherIdle :MonsterStates<MonsterController>
{
    

    private MonsterController manager;
    public override  void Enter(MonsterController manager)
    {
        Debug.Log("ArcherIdle_Enter");
        this.manager=manager;
        //this.manager.monsterMachine.ChangeState(Patrol, manager);
    }
     public override void Update()
     {
      
        
        Debug.Log("ArcherIdle_Update");
     }
  public override void Exit()
  {
        Debug.Log("ArcherIdle_Exit");
  }
}
