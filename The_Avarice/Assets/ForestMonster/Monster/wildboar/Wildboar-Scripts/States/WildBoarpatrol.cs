using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

[CreateAssetMenu(
    fileName = "NewWildBoarState",     // 생성될 에셋 기본 이름
    menuName = "WildBoarStates/patrol" // 메뉴 경로
 
)]
public class WildBoarpatrol : MonsterStates
{
  
    private float movespeed;
    private float patrolTime;
    private float time;
    private Vector3 moveDir;
    private MonsterController manager;
    private Dictionary<string, MonsterStates> WildBoarState;
    [Leein.InspectorName("WildBoarpatrol->NextState")][SerializeField] private string NextState;
    [SerializeField] private string PlayAnimaction;
    
    public override void Initialize(MonsterController manager)
    {
        this.manager = manager;
        movespeed = this.manager.statusManager.movespeed;
        patrolTime = this.manager.statusManager.patrolTime;
        WildBoarState = this.manager.State;
        time = 0;
    }
    public override void Enter()
    {
        Debug.Log("WildBoarpatrol 시작");
       
        PlayMove();
        SetDirection();
    }
    public override void Update()
    {
        HandlePatrolTime(patrolTime);//순찰 끝났는지 체크
        CheckDetectionAndTransition();//플레이어 감지 체크
        MovePatrol(moveDir, movespeed);//이동 동작 실행
    }
    public override void Exit()
    {

    }
   
    private void PlayMove()
    {
        this.manager.aniManager.Play(PlayAnimaction);
    }
    private void SetDirection()
    {
        moveDir = Random.value < 0.5f ? Vector3.right : Vector3.left;
    }
    private void HandlePatrolTime(float patrolTime)
    {
        if ((patrolTime - time) < 0)
        {
            Debug.Log("순찰 끝");
            time = 0;
            manager.MonsterMachine.ChangeState(WildBoarState[NextState], manager);
        }
    }
    private void CheckDetectionAndTransition()
    {
        if (manager.Detectionrange.findcollider != null)
        {
            manager.MonsterMachine.ChangeState(WildBoarState[NextState], manager);
        }
    }
    private void MovePatrol(Vector3 moveDir, float moveSpeed)
    {
        manager.Detectionrange.renderer.flipX = moveDir.x > 0 ? false : true;
        manager.MonsterTrans.position += moveDir * moveSpeed * Time.deltaTime;
        time += Time.deltaTime;
    }
}