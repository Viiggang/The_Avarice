using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "NewWildBoarState",     // 생성될 에셋 기본 이름
    menuName = "WildBoarStates/Idle" // 메뉴 경로
    
)]
public class WildBoarIdle : MonsterStates
{
    [SerializeField] private string PlayAnimaction;
    private MonsterController manager;
    private float delaytime;
    private float time = 0;

    private Dictionary<string, MonsterStates> WildBoarState;
    [Leein.InspectorName("Idle->Chase")][SerializeField] private string Chase;
    [Leein.InspectorName("Idle->Patrol")][SerializeField] private string Patrol;
    public override void Initialize(MonsterController manager)
    {
        this.manager = manager;
        delaytime = this.manager.statusManager.IdleTime;
        WildBoarState = this.manager.State;
    }
    public override void Enter( )
    {
        Debug.Log("WildBoarIdle");
       
        PlayIdle();
    }
    public override void Update()
    {
        ProcessDelay();
    }
    public override void Exit()
    {
       
    }
   
    private void  NextState()
    {
        if (manager.Detectionrange.findcollider == null)
        {
            manager.MonsterMachine.ChangeState(WildBoarState[Patrol],manager);
        }
        //인지 범위에서 플레이어를 찾았으면->추적
        else if (manager.Detectionrange.findcollider != null)
        {
            manager.MonsterMachine.ChangeState(WildBoarState[Chase], manager);
        }
     
    }
    private void PlayIdle()
    {
        manager.aniManager.Play(PlayAnimaction);
    }
    private void ProcessDelay()
    {
        if (time > delaytime)
        {
            time = 0;
            NextState();
        }
        else
        {
            time += Time.deltaTime;
        }
    }
}
