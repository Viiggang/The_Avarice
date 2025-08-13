using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildBoarManager : MonoBehaviour
{
    public MonsterMachine MonsterMachine;//상태머신
    public BoarAniManager aniManager;//애니메이션 매니저
    public BoarStatus statusManager;//상태 매니저
    public WBDetectionrange WBDetectionrange;//인지 범위
    public Transform BoarTrans;

    public WildBoarIdle Idle;
    public WildBoardeath death;
    public WildBoarChase chase;
    public WildBoarAttack attack;
    public WildBoarpatrol patrol;
    void Start()
    {
        MonsterMachine= new MonsterMachine();
        death=new WildBoardeath(this);
        Idle = new WildBoarIdle(this);
        patrol=new WildBoarpatrol(this);
        chase= new WildBoarChase(this);
        attack =new WildBoarAttack(this);   
        MonsterMachine.ChangeState(Idle);
    }
    void Update()
    {
        MonsterMachine.Update();
        
    }
}
