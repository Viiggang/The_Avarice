using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using XNode;

public class BossChase : BaseState<BossController>
{
    #region ��� ���ἱ
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
     ���� �����ִϸ��̼� �� �� �����������
     */
    public override void Enter(BossController Data)
    {
        timer = 0;
        this.Data = Data;
        Tracker = null;
        Bossevents= Data.BossAnimactionEvents;
        //targetChase = null;
        #region �׽�Ʈ
        //NodePort port = GetOutputPort("Next");

        //foreach (NodePort connection in port.GetConnections())
        //{

        //    string portName = connection.node.name; // ����� ��Ʈ �̸�
        //    Debug.Log($" ��Ʈ �̸�: {portName}");
        //}
        #endregion
        //�������� ��ų�� �̾ƿ� size �� offset���� ����
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
    public void Chase(BossController Data)//�����ϴ� �ڵ� 
    {
       BossStatus status = Data.status;
       SpriteRenderer SR_Data = Data.SpriteRenderer;
       BoxCollider2D collider2D = Data.Collider2D;

        //���� ����
        float Dir = Tracker.GetDirection();
        //�������ͽ� ����
        Tracker.SetSkill(CurrentSkill);
       Tracker.SetStatus(status);
        //���� ��ġ ����
        Tracker.SetSize(CurrentSkill.GetSize());
        Tracker.SetOffset(CurrentSkill.GetOffset(Data.SpriteRenderer));
        Tracker.SetCollisonPos(CurrentSkill.GetOffset(SR_Data, Data.BossTransform));

        SetColliderPos(Data, Dir);
        SetBossDirectionFlip(Data, Dir);
        Tracker.Chase();
        
        //�ݶ��̴� ��ġ ����
      
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

    public void Cycle()//4�ʸ��� ������ ��ų  �����Ѵ�.
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
