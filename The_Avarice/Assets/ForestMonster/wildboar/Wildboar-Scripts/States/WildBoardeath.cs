using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildBoardeath : IState
{
    public string name { get; set; }
    private WildBoarManager manager;
    public WildBoardeath(WildBoarManager manager)
    {
        this.manager = manager;
    }
    public void Enter()
    {

    }
    public void Update()
    {

    }
    public void Exit()
    {

    }
}
