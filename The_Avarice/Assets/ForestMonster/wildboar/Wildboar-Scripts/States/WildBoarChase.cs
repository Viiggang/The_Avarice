using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(
    fileName = "NewWildBoarState",     // 생성될 에셋 기본 이름
    menuName = "WildBoarStates/Chase" // 메뉴 경로
   
)]
public class WildBoarChase : MonsterStates<MonsterManager>
{
   
    private MonsterManager manager;
    private Transform target;
    private float AttackDistance = 0;
    public Dictionary<string, MonsterStates<MonsterManager>> WildBoarState;
    [Leein.InspectorName("Chase->Idle")][SerializeField]private string idle;
    [Leein.InspectorName("Chase->Attack")][SerializeField] private string attack;
    [SerializeField] private string PlayAnimaction;
    public override void Enter(MonsterManager manager)
    {
        Initialize(manager);
        PlayMove();
    }
    public override void Update()
    {
        if (CheckIdleTransition()) return;
        if (CheckAttackTransition()) return;
        ChaseTarget();
    }
    public override  void Exit()
    {

    }
    public override void Initialize(MonsterManager manager)
    {
        this.manager = manager;
        WildBoarState = manager.State;
        AttackDistance = manager.statusManager.AttckDistance;

        var PlayerCollider = manager.Detectionrange.findcollider;
        if (PlayerCollider != null)
        {
            target = PlayerCollider.gameObject.GetComponent<Transform>();
            if (target == null)
            {
                Debug.Log("WildBoarChase 30번째 줄 확인");
            }
        }
        else
        {
            manager.MonsterMachine.ChangeState(WildBoarState[idle], manager);
        }
    }
    private void PlayMove()
    {
        manager.aniManager.Play(PlayAnimaction);
    }
    private bool CheckIdleTransition()
    {
        if (manager.Detectionrange.findcollider == null)
        {
            manager.MonsterMachine.ChangeState(WildBoarState[idle], manager);
            return true; 
        }
        return false;
    }
    private bool CheckAttackTransition()
    {
        float distanceX = Mathf.Abs(manager.MonsterTrans.position.x - target.position.x);

        if (distanceX < AttackDistance)
        {
            manager.MonsterMachine.ChangeState(WildBoarState[attack], manager);
            return true; // 상태 변경 시 Update 로직 중단
        }
        return false;
    }
    private void ChaseTarget()
    {
        float dirX = Mathf.Sign(target.position.x - manager.MonsterTrans.position.x);

        // 스프라이트 방향 전환
        manager.Detectionrange.renderer.flipX = dirX < 0;

        // 좌우 이동
        Vector3 moveDir = new Vector3(dirX, 0, 0);
        manager.MonsterTrans.position += moveDir * manager.statusManager.movespeed * Time.deltaTime;
    }
}
