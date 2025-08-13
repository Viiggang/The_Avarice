using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildBoarAttack : IState
{
    private WildBoarManager manager;
    public WildBoarAttack(WildBoarManager manager)
    {
        this.manager = manager;
    }
    public void Enter()
    {
        manager.aniManager.Play_Attack();
    }
    public void Update()
    {

    }
    public void Exit()
    {

    }
}
