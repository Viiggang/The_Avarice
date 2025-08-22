using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(
    fileName = "NewWildBoarState",     // 생성될 에셋 기본 이름
    menuName = "WildBoarStates/death" // 메뉴 경로

)]
public class WildBoardeath :MonsterStates
{
    private MonsterController manager;
    public override void Initialize(MonsterController manager)
    {
        this.manager = manager;
    }
     
    public override void Update()
    {

    }
    public override  void Exit()
    {

    }
    
}
