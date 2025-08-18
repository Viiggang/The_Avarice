using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "idle", menuName = "ArcherStates/idle")]
public class ArcherIdle :MonsterStates<MonsterController>
{

    private MonsterController manager;
    private Dictionary<string, MonsterStates<MonsterController>> State;
    private MonsterMachine<MonsterController> MonsterMachine;//상태머신
    [Leein.InspectorName("대기->순찰")]public string patrol;
    [Leein.InspectorName("대기->추적")]public string chase;
    private float idletime=0;
    private float time = 0;
    [SerializeField]private string idle;
    public override  void Enter(MonsterController manager)
    {
        Debug.Log("ArcherIdle_Enter");
        Initialize(manager);
        play();
    }
     public override void Update()
     {
        
        if(time>idletime)
        {
            time = 0;
            Think();
        }
        else
        {
            time += Time.deltaTime;
        }
     }
     public override void Exit()
     {
        Debug.Log("ArcherIdle_Exit");
     }

    public override void Initialize(MonsterController manager)
    {
        this.manager = manager;
        State = manager.State;
        MonsterMachine=manager.MonsterMachine;
        idletime = this.manager.statusManager.IdleTime;
    }
    private void Think()
    {
        bool findplayer = manager.Detectionrange.findcollider;
        if (findplayer)
        {
            MonsterMachine.ChangeState(State[chase],manager);
        }
        else
        {
            MonsterMachine.ChangeState(State[patrol], manager);
        }
    }
    private void play()
    {
        manager.aniManager.Play(idle);
    }

}
