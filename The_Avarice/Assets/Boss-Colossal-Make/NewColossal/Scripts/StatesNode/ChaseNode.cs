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
        boss = ColossalHandler.Instance.transformManager.Boss;
        Player = ColossalHandler.Instance.transformManager.Player;
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
            if(direction.x<0)
            {
                ColossalHandler.Instance.spriteManager.spriteRenderer.flipX = true;
            }
            else
            {
                ColossalHandler.Instance.spriteManager.spriteRenderer.flipX = false;
            }
            boss.transform.position += direction * speed * Time.deltaTime; 

        }
    }
    public override void Exit()
    {
        Debug.Log("ChaseNodeExit");
    }
    //IEnumerator TryFindPlayerRoutine()
    //{
    //    while (Player == null)
    //    {
    //        TryFindPlayer();  // 플레이어 찾기 시도 함수
    //        yield return new WaitForSeconds(1f);  // 1초마다 반복
    //    }
    //    // 플레이어 찾으면 추격 시작 가능
    //}
   
}
