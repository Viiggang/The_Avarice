using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(
    fileName = "Chase",     // ������ ���� �⺻ �̸�
    menuName = "Monster/StateData/Chase" // �޴� ���

)]
public class WildBoarChase : MonsterStates
{

    [Leein.InspectorName("Chase->Idle")][SerializeField]private string idle;
    [Leein.InspectorName("Chase->Attack")][SerializeField] private string attack;
    public override void Enter(MonsterController controller)
    {
         
       bool flag= CheckAttackTransition(controller);
       if (flag) return;

        PlayMove(controller);
    }
    public override void Excute(MonsterController controller)
    {
        CheckIdleTransition(controller);
        CheckAttackTransition(controller);
        ChaseTarget(controller);
    }
    public override  void Exit(MonsterController controller)
    {

    }
    //private void SetTarget(MonsterController controller)
    //{
    //    MsDetectionRange Detection = controller.Detection;
    //    if (Detection.findcollider == null)
    //    {
    //        controller.MonsterMachine.ChangeState(controller.State["idle"], controller);
    //    }
       
    //}
    private void PlayMove(MonsterController controller)
    {
        MonsterAniController animator = controller.aniManager;
        animator.Play("move");
    }
    private void CheckIdleTransition(MonsterController controller)
    {//���� ���� �ȿ� ���Ͱ� ���ٸ�
        MsDetectionRange Detectionrange = controller.Detection;
        bool hasCollider = Detectionrange.findcollider==null;
        if (hasCollider)
        {
            controller.MonsterMachine.ChangeState(controller.State[idle], controller);
        
        }
        
    }
    private bool CheckAttackTransition(MonsterController controller)
    {
        float distanceX = Mathf.Abs(controller.MonsterTrans.position.x - controller.target.position.x);
        bool canAttack = (distanceX < controller.statusManager.AttackDistance);
        if (canAttack)
        {
            controller.MonsterMachine.ChangeState(controller.State[attack], controller);
            return true;

        }
        return false;
    }
    private void ChaseTarget(MonsterController controller)
    {
        float dirX = Mathf.Sign(controller.target.position.x - controller.MonsterTrans.position.x);

        // ��������Ʈ ���� ��ȯ
        controller.Detection.renderer.flipX = dirX < 0;

        // �¿� �̵�
        Vector3 moveDir = new Vector3(dirX, 0, 0);
        controller.MonsterTrans.position += moveDir * controller.statusManager.movespeed * Time.deltaTime;
    }
}
