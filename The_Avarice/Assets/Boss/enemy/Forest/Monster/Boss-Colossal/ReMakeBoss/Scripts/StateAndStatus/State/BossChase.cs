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
    Transform BossPos, Target;
    public override void Enter(BossController Data)
    {
       
        InitializeBossState(Data, out BossPos, out Target);
        InitializeTracker(Data, BossPos, Target);
        #region �׽�Ʈ
        //NodePort port = GetOutputPort("Next");

        //foreach (NodePort connection in port.GetConnections())
        //{

        //    string portName = connection.node.name; // ����� ��Ʈ �̸�
        //    Debug.Log($" ��Ʈ �̸�: {portName}");
        //}
        #endregion
    }
    public override void Excute(BossController Data)
    {
        if (Target == null) return;
        Cycle();
        Chase(Data); 
    }

    public override void Exit(BossController Data)
    {
        ClearState();
    }

  

    public void Chase(BossController Data)//�����ϴ� �ڵ� 
    {
        if (CurrentSkill is null || CurrentSkill.CollisionData is null) return;

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
    }

    private void SetColliderPos(BossController data, float dir)
    {//���� ���� �� �� Hit�ݶ��̴� ��ġ �����մϴ�. 
        BoxCollider2D box = data.Collider2D;
        bool isFlipped = dir < 0;//������ �������� ������ ���� �Ǻ� -1 ���� 1������
        Vector2 offset = box.offset;
        offset.x = isFlipped ? Mathf.Abs(offset.x) : -Mathf.Abs(offset.x);
        box.offset = offset;
    }
    private void SetBossDirectionFlip(BossController data, float dir)
    {
        bool isFlipped = dir < 0;
        data.SpriteRenderer.flipX = isFlipped;
    }

    public void Cycle()
    { 
        //4�ʸ��� ������ ��ų  �����Ѵ�.
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
    private void InitializeTracker(BossController Data, Transform BossPos, Transform Target)
    {
        if (Tracker.Target == null)
        {
            SpriteRenderer SR_Data = Data.SpriteRenderer;
            BoxCollider2D collider2D = Data.Collider2D;
            Tracker = new TargetTracker(Target, BossPos, Player);
            Tracker.SetAction(Attack);
        }
    }
    private void InitializeBossState(BossController Data, out Transform BossPos, out Transform Target)
    {
        timer = 0;
        this.Data = Data;

        Bossevents = Data.BossAnimactionEvents;
        BossPos = Data.BossTransform;
        Target = Data.TargetPos;
        BossSkillGroup = Data.BossSkillGroup;
    }
    private void ClearState()
    {
        timer = 0;
        CurrentSkill = null;
    }
}
