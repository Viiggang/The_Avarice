using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class BossAnimactionEvents : MonsterAniEvents
{
    [Header("여기서 부터는 보스 데이터")]
    public BossController BossController;
    public BossSkill CurrentSkill;
    public BossSkillGroup SkillGroup;
    public BossStage BossStage;
    public Transform BossTransfrom;
    void Start()
    {
        base.Start();
    }
    public void NextNode(string NextNodeName)
    {
       
        BossStateMachine stateMachine = BossController.stateMachine;
        if(stateMachine ==null)
        {
            Debug.Log("stateMachine null");
        }
        stateMachine.SetNextState(NextNodeName);
      
    }
    public void ColossalAttack(string trigger)
    {
        SetSkill();//현재 클래스 내부에 선택된 공격 설정
        SetAttackColliosnData();//현재 스킬에서 Scale값 만큼 보정
        DicAniEvents[trigger].Execute(BossController, attackCollisionData, BossStage);
    }

    public void OnWarningWindow()
    {
        ColossalEvent.Instance.UItrigger();
    }
    public void SetSkill()
    {
        CurrentSkill = SkillGroup.CurrentSkill;
    }
    public void SetAttackColliosnData()
    {
      Vector2 TempOffset= CurrentSkill.CollisionData.offset;
      Vector2 TempSize  = CurrentSkill.CollisionData.Size;

        attackCollisionData.offset= new Vector2(
            BossTransfrom.position.x + (attackCollisionData.offset.x * Mathf.Abs(BossTransfrom.lossyScale.x)),
            BossTransfrom.position.y + (attackCollisionData.offset.y * Mathf.Abs(BossTransfrom.lossyScale.y))
        );

        attackCollisionData.size=new Vector2(
            BossTransfrom.lossyScale.x * TempSize.x, 
            BossTransfrom.lossyScale.y * TempSize.y);
    }
}
