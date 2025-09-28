using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

[CreateAssetMenu(
    fileName = "patrol",     // 생성될 에셋 기본 이름
    menuName = "Monster/StateData/patrol" // 메뉴 경로

)]
public class WildBoarpatrol : MonsterStates
{

    [SerializeField] private string PlayAnimaction;
    
    
    public override void Enter(MonsterController controller)
    {
        Debug.Log("WildBoarpatrol 시작");
        controller.StartState = "patrol";
       
        PlayMove(controller);
        SetDirection(controller);
    }
    public override void Excute(MonsterController controller)
    {
        HandlePatrolTime(controller);//순찰 끝났는지 체크
        CheckDetectionAndTransition(controller);//플레이어 감지 체크
        MovePatrol(controller);//이동 동작 실행
    }
    public override void Exit(MonsterController controller)
    {

    }
   
    private void PlayMove(MonsterController controller)
    {
        MonsterAniController aniManager=controller.aniManager;
        aniManager.Play(PlayAnimaction);
    }
    private void SetDirection(MonsterController controller)
    {
        MonsterStatus status = controller.statusManager;
        status.moveDir = Random.value < 0.5f ? Vector3.right : Vector3.left;
    }
    private void HandlePatrolTime(MonsterController controller)
    {
        MonsterStatus status=controller.statusManager;
        MonsterMachine < MonsterController> MonsterMachine= controller.MonsterMachine;
        if ((status.patrolTime - status.time) < 0)
        {
            Debug.Log("순찰 끝");
            status.time = 0;
           MonsterMachine.ChangeState(controller.State["idle"], controller);
        }
    }
    private void CheckDetectionAndTransition(MonsterController controller)
    {
        MsDetectionRange Detection = controller.Detection;
        MonsterMachine<MonsterController> MonsterMachine = controller.MonsterMachine;
        if ( Detection.findcollider != null)
        {
            controller.target = Detection.findcollider.transform;
           MonsterMachine.ChangeState(controller.State["idle"], controller);
        }
    }
    private void MovePatrol(MonsterController controller)
    {
        MonsterStatus Status = controller.statusManager;
        MsDetectionRange Detection = controller.Detection;
        Transform MonsterTrans = controller.MonsterTrans;

         Detection.renderer.flipX = Status.moveDir.x > 0 ? false : true;
         MonsterTrans.position += Status. moveDir * Status.movespeed * Time.deltaTime;
        Status.time += Time.deltaTime;
    }
}