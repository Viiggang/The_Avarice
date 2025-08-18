using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
[CreateAssetMenu(fileName = "Chase", menuName = "ArcherStates/Chase")]
public class ArcherChase : MonsterStates<MonsterController>
{
    private MonsterController manager;
    private float movespeed;
    private Transform player;
    private Transform mytransform;
    private float AttckDistance;
    private SpriteRenderer Spriteflip;
    private Vector3 dir;//방향
    private Dictionary<string, MonsterStates<MonsterController>> State;
    private MonsterMachine<MonsterController> MonsterMachine;//상태머신
    public string move;
    public string idle;
    public string attack;
    private Vector3 moveDir;
    public override void Enter(MonsterController manager)
    {
        Initialize(manager);
        

    }
    public override void Update()
    {
        if (CheckIdleTransition()) return;
        if (CheckAttackTransition()) return;
        ChaseTarget();
    }
    public override void Exit()
    {

    }
    public override void Initialize(MonsterController manager)
    {
      if(this.manager ==null)
      {
            this.manager=manager;
            movespeed = this.manager.statusManager.movespeed;
            player= this.manager.Detectionrange.findcollider.GetComponent<Transform>();
            mytransform = this.manager.MonsterTrans;
            AttckDistance = this.manager.statusManager.AttckDistance;
            State = this.manager.State;
            MonsterMachine = this.manager.MonsterMachine;
        }
    }
    private void PlayMove()//chase상태의 애니메이션 설정
    {
        manager.aniManager.Play(move);
    }
    private bool CheckIdleTransition()
    {
        if (manager.Detectionrange.findcollider == null)
        {
            manager.MonsterMachine.ChangeState(manager.State[idle], manager);
            return true;
        }
        return false;
    }
    private bool CheckAttackTransition()
    {
        float distanceX = Mathf.Abs(manager.MonsterTrans.position.x - player.position.x);

        if (distanceX < AttckDistance)
        {
            manager.MonsterMachine.ChangeState(manager.State[attack], manager);
            return true; // 상태 변경 시 Update 로직 중단
        }
        return false;
    }
    private void ChaseTarget()
    {
        float dirX = Mathf.Sign(player.position.x - manager.MonsterTrans.position.x);

        // 스프라이트 방향 전환
        manager.Detectionrange.renderer.flipX = dirX < 0;

        // 좌우 이동
        Vector3 moveDir = new Vector3(dirX, 0, 0);
        manager.MonsterTrans.position += moveDir * manager.statusManager.movespeed * Time.deltaTime;
    }
}