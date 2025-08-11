using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMachine  
{

    public IState currentState;
    public void ChangeState(IState newState)
    {
        // 기존 상태 종료
        currentState?.Exit();

        // 상태 변경
        currentState = newState;

        // 새 상태 진입
        currentState.Enter();
    }


    // Update is called once per frame
   public void Update()
    {
        currentState.Update();
    }
  
}
