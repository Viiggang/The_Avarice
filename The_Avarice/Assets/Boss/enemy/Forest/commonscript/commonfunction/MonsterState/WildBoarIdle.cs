using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "Idle",     // ������ ���� �⺻ �̸�
    menuName = "Monster/StateData/Idle" // �޴� ���

)]
public class WildBoarIdle : MonsterStates
{
    [SerializeField] private string PlayAnimaction;
   
    [Leein.InspectorName("Idle->Chase")][SerializeField] private string Chase;
    [Leein.InspectorName("Idle->Patrol")][SerializeField] private string Patrol;
  
    public override void Enter(MonsterController controller)
    {
        controller.StartState = "idle";
        PlayIdle(controller);
    }
    public override void Excute(MonsterController controller)
    {
        ProcessDelay(controller);
    }
    public override void Exit(MonsterController controller)
    {
       
    }
   
    private void  NextState(MonsterController controller)
    {
        MsDetectionRange Detectionrange = controller.Detection;
        MonsterMachine< MonsterController > MonsterMachine= controller.MonsterMachine;
        if (Detectionrange.findcollider == null)
        {
           MonsterMachine.ChangeState(controller.State[Patrol], controller);
        }
        //���� �������� �÷��̾ ã������->����
        else if (Detectionrange.findcollider != null)
        {
            controller.target = Detectionrange.findcollider.transform;
            MonsterMachine.ChangeState(controller.State[Chase], controller);
        }
     
    }
    private void PlayIdle(MonsterController controller)
    {
        MonsterAniController animator = controller.aniManager;
        animator.Play(PlayAnimaction);
    }
    private void ProcessDelay(MonsterController controller)
    {
        MonsterStatus status = controller.statusManager;
        if (status.time > status.IdleTime)
        {
            status.time = 0;
            NextState(controller);
        }
        else
        {
            status.time += Time.deltaTime;
        }
    }
}
