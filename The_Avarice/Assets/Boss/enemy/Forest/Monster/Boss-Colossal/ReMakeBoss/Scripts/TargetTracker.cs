using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TargetTracker 
{
    struct time
    {
        public float DelayTime;
        public float CurrentTime;
    }
    [Leein.InspectorName("플레이어 트랜스 폼 ")]public Transform Target;
    [Leein.InspectorName("보스 트랜스 폼 ")]public Transform tracker;
    [Leein.InspectorName("스테이터스")]public BossStatus status;

    LayerMask Player;
    Vector3 CollisonPos;
    //physic2D 용 데이터
    Vector2 PhysicSize;
    Vector2 PhysicOffset;

    public Action NextNode;
    time physicsCalculationLatency;
    CapsuleCollider2D[] Collider2Ds;

    BossSkill BossSkill;

    Collider2D hitCollider;
    public TargetTracker (Transform _Target, Transform tracker,LayerMask layerMask)
    {
        this.Target = _Target;
        this.tracker = tracker;
        Collider2Ds = new CapsuleCollider2D[1];
        Player = layerMask;

        physicsCalculationLatency.DelayTime = 0.1f;

    }
  
    //추적하는 함수
    public void Chase()
    {
        float degree = 0f;
        GizmoTest.Instance.Set(PhysicOffset, PhysicSize);
        if (BossSkill.CollisionData.name == "purgeblow")
        {
            
          
            NextNode.Invoke(); // 공격 트리거
            return;
        }
       
        // 1. 방향 계산
        float directionX = Mathf.Sign(Target.position.x - CollisonPos.x);

        // 2. 플레이어 감지 체크
        //bool isPhysicsCheckReady = physicsCalculationLatency.CurrentTime >= physicsCalculationLatency.DelayTime;
        
        bool isPlayerDetected = false;
        hitCollider = Physics2D.OverlapBox(
            PhysicOffset,//위치
            PhysicSize,//사이즈
            degree,//각도
            Player//플레이어 레이어
        );
         if(hitCollider !=null)
           // Debug.Log(hitCollider.name);
        
       
       //충돌
       isPlayerDetected = hitCollider!=null;
       if (isPlayerDetected)
       {
           NextNode.Invoke(); // 공격 트리거
           return;  
       }
       

        // 3. 플레이어 못 찾았으면 추적
        if(Mathf.Abs(PhysicOffset.x- Target.position.x)>=0.5f)
            tracker.position += new Vector3(directionX, 0f, 0f) * status.speed * Time.deltaTime;
    }

    public float GetDirection() => Mathf.Sign(Target.position.x- CollisonPos.x );//보스 기준 추적 방향 (왼쪽 오른쪽 방향 구하기)
    

    public void SetCollisonPos(Vector3 Data) => CollisonPos = Data;
   
    public void SetStatus(BossStatus Status) => this.status = Status;
  
    // 감지되는 영역에 플레이어가 들어오면 실행시킬 함수
    public void SetAction(Action Data) => NextNode = Data;

    public void SetSize(Vector2 size) => this.PhysicSize = new Vector2(tracker.lossyScale.x * size.x, tracker.lossyScale.y * size.y);
    
    public void SetOffset(Vector2 Offset)
    {//

        this.PhysicOffset = new Vector2(
            tracker.position.x + (Offset.x* Mathf.Abs(tracker.lossyScale.x)), 
            tracker.position.y + (Offset.y* Mathf.Abs(tracker.lossyScale.y))
            );
    }
    public void SetSkill(BossSkill skill) => this.BossSkill = skill;

    public Vector2 GetOffset() => this.PhysicOffset; 
    public Vector2 GetSize()=>this.PhysicSize;
}
