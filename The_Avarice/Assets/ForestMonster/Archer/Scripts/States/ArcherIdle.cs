using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "idle", menuName = "ArcherStates/idle")]
public class ArcherIdle :MonsterStates 
{
    [SerializeField] private string Chase;
    [SerializeField] private string Patrol;
    private ArcherManager manager;
    public override  void Enter(ArcherManager manager)
    {
        Debug.Log("ArcherIdle_Enter");
        this.manager = manager;
        manager.monsterMachine.ChangeState(manager.StatusList[Patrol]);
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
