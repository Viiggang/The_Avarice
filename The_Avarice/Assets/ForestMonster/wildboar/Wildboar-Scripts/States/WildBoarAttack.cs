using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(
    fileName = "NewWildBoarState",     // 생성될 에셋 기본 이름
    menuName = "WildBoarStates/Attack" // 메뉴 경로

)]
public class WildBoarAttack : MonsterStates<MonsterManager>
{
    [SerializeField]private string PlayAnimaction;
    private MonsterManager manager;
    public override void Enter(MonsterManager manager)
    {
        Initialize(manager);
        PlayAttack();
    }
    public override void Update()
    {

    }
    public override void Exit()
    {

    }
    public override void Initialize(MonsterManager manager)
    {
        if (this.manager == null)
        {
            this.manager = manager;
        }
    }
    private void PlayAttack()
    {
        manager.aniManager.Play(PlayAnimaction);
    }
}
