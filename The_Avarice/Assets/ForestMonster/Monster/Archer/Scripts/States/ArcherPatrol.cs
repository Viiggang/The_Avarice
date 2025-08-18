using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(fileName = "patrol", menuName = "ArcherStates/Patrol")]
public class ArcherPatrol : MonsterStates<MonsterController>
{
    private Transform findPlayer;
    private float movespeed;
    private float patrolTime = 0;
    private MonsterController manager;
    private float time = 0;
    [Leein.InspectorName("실행할 애니메이션 이름")][SerializeField]private string PatrolPlay;
    public Dictionary<string, MonsterStates<MonsterController>> State;//상태관리용
    private Vector3 moveDir;
    [SerializeField]private string NextState;
 
   
    public override void Enter(MonsterController manager)
    {
        Debug.Log("ArcherPatrol_Enter");
        Initialize(manager);
        SetDirection();
        play();
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
    public override void Initialize(MonsterController manager)
    {
        this.manager = manager;
        patrolTime = this.manager.statusManager.patrolTime;
        movespeed = this.manager.statusManager.movespeed;
        State = this.manager.State;
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
            manager.MonsterMachine.ChangeState(manager.State[NextState], manager);
        }
    }
    private void CheckDetectionAndTransition()
    {
        if (manager.Detectionrange.findcollider != null)
        {
            manager.MonsterMachine.ChangeState(manager.State[NextState], manager);
        }
    }
    private void MovePatrol(Vector3 moveDir, float moveSpeed)
    {
        manager.Detectionrange.renderer.flipX = moveDir.x > 0 ? false : true;
        manager.MonsterTrans.position += moveDir * moveSpeed * Time.deltaTime;
        time += Time.deltaTime;
    }
    private void play()
    {
        this.manager.aniManager.Play(PatrolPlay);
    }
}