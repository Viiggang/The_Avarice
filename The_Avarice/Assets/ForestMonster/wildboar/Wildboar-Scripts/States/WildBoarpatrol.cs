using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildBoarpatrol : IState
{
    public string name { get; set; }
    private float movespeed;
    private float patrolTime;
    private float time;
    private Vector3 moveDir;
    private WildBoarManager manager;
    public WildBoarpatrol(WildBoarManager manager)
    {
        this.manager = manager;
        movespeed= this.manager.statusManager.movespeed;
        patrolTime = this.manager.statusManager.patrolTime;
    }
    public void Enter()
    {
        Debug.Log("WildBoarpatrol Ω√¿€");
        manager.aniManager.Play_Move();
        moveDir = Random.value < 0.5f ? Vector3.right : Vector3.left;
    }
    public void Update()
    {
        if((patrolTime-time)<0)
        {
            Debug.Log("º¯¬˚ ≥°");
            time = 0;
            manager.MonsterMachine.ChangeState(manager.Idle);
        }
        else
        {
            if (manager.WBDetectionrange.findcollider != null)
            {
                manager.MonsterMachine.ChangeState(manager.Idle);
            }
            manager.WBDetectionrange.renderer.flipX = moveDir.x > 0? false : true;
            manager.BoarTrans.position += moveDir * movespeed * Time.deltaTime;
            time += Time.deltaTime;
        }
    }
    public void Exit()
    {

    }
}