using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using XNode;

public class BossChase : BaseState<BossController>
{
    #region 노드 연결선
    [Output(connectionType = ConnectionType.Multiple)] public BaseState<BossController> Next;
    [Input] public BaseState<BossController> Input;
    #endregion


    public float timer;
    public string targetNodeName;
    public BossSkill CurrentSkill;
    public BossSkillGroup BossSkillGroup;
    public TargetTracker Tracker;
    
    BossController Data;
    public LayerMask Player;
    BossAnimactionEvents Bossevents;
    /*
     현재 보스애니메이션 에 값 설정해줘야함
     */
    public override void Enter(BossController Data)
    {
        timer = 0;
        this.Data = Data;
        Tracker = null;
        Bossevents= Data.BossAnimactionEvents;
        //targetChase = null;
        #region 테스트
        //NodePort port = GetOutputPort("Next");

        //foreach (NodePort connection in port.GetConnections())
        //{

        //    string portName = connection.node.name; // 연결된 포트 이름
        //    Debug.Log($" 포트 이름: {portName}");
        //}
        #endregion
        //랜덤으로 스킬을 뽑아옴 size 와 offset받을 예정
        Transform BossPos = Data.BossTransform;
        Transform Target =Data.DetectionRange.findcollider.GetComponent<Transform>();
        BossSkillGroup = Data.BossSkillGroup;

       

        if (Tracker ==null)
        {
            SpriteRenderer SR_Data = Data.SpriteRenderer;
            BoxCollider2D collider2D = Data.Collider2D;
            Tracker = new TargetTracker(Target, BossPos, Player);
            
            
            Tracker.SetAction(Attack);
        }
           

    }
    public override void Excute(BossController Data)
    {
     
        Cycle();
        if (CurrentSkill is null || CurrentSkill.CollisionData  is  null)return;
        Chase(Data);
    }

    public override void Exit(BossController Data)
    {
        timer = 0;
        CurrentSkill = null;
    }
    /*
     
     */
    public void Chase(BossController Data)//추적하는 코드 
    {
       BossStatus status = Data.status;
       SpriteRenderer SR_Data = Data.SpriteRenderer;
       BoxCollider2D collider2D = Data.Collider2D;

        //방향 설정
        float Dir = Tracker.GetDirection();
        //스테이터스 셋팅
        Tracker.SetSkill(CurrentSkill);
       Tracker.SetStatus(status);
        //추적 위치 설정
        Tracker.SetSize(CurrentSkill.GetSize());
        Tracker.SetOffset(CurrentSkill.GetOffset(Data.SpriteRenderer));
        Tracker.SetCollisonPos(CurrentSkill.GetOffset(SR_Data, Data.BossTransform));

        SetColliderPos(Data, Dir);
        SetBossDirectionFlip(Data, Dir);
        Tracker.Chase();
        
        //콜라이더 위치 설정
      
    }

    private void SetColliderPos(BossController data, float dir)
    {
        BoxCollider2D box = data.Collider2D;
        bool isFlipped = dir < 0;

        //data.SpriteRenderer.flipX = isFlipped;

        Vector2 offset = box.offset;
        offset.x = isFlipped ? Mathf.Abs(offset.x) : -Mathf.Abs(offset.x);
        box.offset = offset;
    }
    private void SetBossDirectionFlip(BossController data, float dir)
    {
        bool isFlipped = dir < 0;

        data.SpriteRenderer.flipX = isFlipped;
    }

    public void Cycle()//4초마다 공격할 스킬  지정한다.
    {  
        const float Delay = 4.0f;
        bool isChange = timer >= Delay;
        if(isChange)
        {
            timer = 0f;
            CurrentSkill = BossSkillGroup.GetRandomSkill();
           
            BossSkillGroup.data = CurrentSkill.CollisionData;
            
        }
        else
        {
            timer += Time.deltaTime;
        }
    }
    public void Attack()
    {
        Bossevents.SetAttackColliosnData(Tracker.GetOffset(), Tracker.GetSize());
        BossStateMachine machine = Data.stateMachine;
        machine.SetNextState("Next", CurrentSkill.GetNodeName());
        CurrentSkill = null;


    }
}
