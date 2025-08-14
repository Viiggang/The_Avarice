using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildBoarChase : IState
{
    private WildBoarManager manager;
    private Transform target;
    private float AttackDistance = 0;
    public WildBoarChase(WildBoarManager manager)
    {
        this.manager = manager;
    }
    public void Enter()
    {
        manager.aniManager.Play_Move();
        AttackDistance = manager.statusManager.AttckDistance;
        var PlayerCollider = manager.WBDetectionrange.findcollider;
        if(PlayerCollider !=null)
        {
            target = PlayerCollider.gameObject.GetComponent<Transform>();
        }
       
    }
    public void Update()
    {
        float dirX = Mathf.Sign(target.position.x - manager.BoarTrans.position.x);
        //탐지 범위 안에 있는가? findcollider 값 확인
        if (manager.WBDetectionrange.findcollider == null)
        {
            manager.MonsterMachine.ChangeState(manager.patrol);
        }
        //공격 범위 안에 들었는가? 거리 좁히기
        else if(Mathf.Abs(manager.BoarTrans.position.x-target.position.x)< AttackDistance)
        {
            Debug.Log("공격");
            manager.MonsterMachine.ChangeState(manager.attack);
        }
        else
        { //추적
          // 스프라이트 방향 전환
            manager.WBDetectionrange.renderer.flipX = dirX < 0;

            Vector3 moveDir = new Vector3(dirX, 0, 0); // 좌우 방향만
            manager.BoarTrans.position += moveDir * manager.statusManager.movespeed * Time.deltaTime;

         
        }
      
    }
    public void Exit()
    {

    }
}
