using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class ChaseNode : BaseState
{
    [Input] public BaseState input;
    public Sprite[] sprites;
    private UnityEngine.Transform boss;
    private UnityEngine.Transform Player;
    private float speed;
    public override void Enter()
    {
        speed = 0.5f;
        Debug.Log("ChaseNodeEnter");
        boss = ColossalHandler.Instance.Boss;
        Player = ColossalHandler.Instance.targetPlayer;
    }
    public override void Excute()
    {
        Debug.Log("ChaseNodeExcute");
        if (ColossalHandler.Instance.IsNear())
        {
            NodeMachine.Instance.SetNextState("next");
        }
        else
        {

            Vector3 direction = (Player.position - boss.transform.position).normalized;
            boss.transform.position += new Vector3(direction.x, 0f, 0f) * speed * Time.deltaTime; 

        }
    }
    public override void Exit()
    {

    }
}
