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
       
        DicAniEvents[trigger].Execute(BossController, attackCollisionData, BossStage);
    }

    public void OnWarningWindow()
    {
        ColossalEvent.Instance.UItrigger();
    }
  
    public void SetAttackColliosnData(Vector2 TempOffset, Vector2 TempSize)
    {
        attackCollisionData.offset = TempOffset;
        attackCollisionData.size = TempSize;



    }
}
