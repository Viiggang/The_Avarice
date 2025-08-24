using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(
    fileName = "Chase",     // 생성될 에셋 기본 이름
    menuName = "Monster/StateData/Chase" // 메뉴 경로

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
        MonsterAniManager animator = controller.aniManager;
        animator.Play("move");
    }
    private void CheckIdleTransition(MonsterController controller)
    {
        MsDetectionRange Detectionrange = controller.Detection;
        bool hasCollider = Detectionrange.findcollider !=null;
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

        // 스프라이트 방향 전환
        controller.Detection.renderer.flipX = dirX < 0;

        // 좌우 이동
        Vector3 moveDir = new Vector3(dirX, 0, 0);
        controller.MonsterTrans.position += moveDir * controller.statusManager.movespeed * Time.deltaTime;
    }
}
