using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildBoarIdle : IState
{
    public string name { get; set; }
    private WildBoarManager manager;
    private float delaytime;
    private float time = 0;
    public WildBoarIdle(WildBoarManager manager)
    {  
      this.manager = manager;
        delaytime = this.manager.statusManager.IdleTime;
    }
    public void Enter()
    {
         manager.aniManager.Play_Idle();
        Debug.Log("WildBoarIdle");
       

    }
    public void Update()
    {
        if(time>delaytime)
        {
            time = 0;
            NextState();
        }
        else
        {
            time += Time.deltaTime;
        }
    }
    public void Exit()
    {
       
    }
    private void  NextState()
    {
        if (manager.WBDetectionrange.findcollider == null)
        {
            manager.MonsterMachine.ChangeState(manager.patrol);
        }
        //인지 범위에서 플레이어를 찾았으면->추적
        else if (manager.WBDetectionrange.findcollider != null)
        {
            manager.MonsterMachine.ChangeState(manager.chase);
        }
    }
}
