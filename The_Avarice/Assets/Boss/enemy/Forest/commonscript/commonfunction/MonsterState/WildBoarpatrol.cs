using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

[CreateAssetMenu(
    fileName = "patrol",     // ������ ���� �⺻ �̸�
    menuName = "Monster/StateData/patrol" // �޴� ���

)]
public class WildBoarpatrol : MonsterStates
{

    [SerializeField] private string PlayAnimaction;
    
    
    public override void Enter(MonsterController controller)
    {
        Debug.Log("WildBoarpatrol ����");
        controller.StartState = "patrol";
       
        PlayMove(controller);
        SetDirection(controller);
    }
    public override void Excute(MonsterController controller)
    {
        HandlePatrolTime(controller);//���� �������� üũ
        CheckDetectionAndTransition(controller);//�÷��̾� ���� üũ
        MovePatrol(controller);//�̵� ���� ����
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
            Debug.Log("���� ��");
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