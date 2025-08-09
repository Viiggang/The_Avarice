using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pattern2 :BaseState
{
    [Input] public BaseState input;
    [Output] public BaseState Attack1;
    [Output] public BaseState Attack2;
    [Output] public BaseState Attack3;
    [Output] public BaseState Attack4;
    public override void Enter()
    {
        SetStage();
        RandomAttack();
    }
    public override void Excute()
    {

    }
    public override void Exit()
    {

    }
    private void SetStage()
    {
        //이미 한번 설정 했으면 돌아감
        if (ColossalHandler.Instance.currentStage == Stage.Stage2) return; 
        ColossalHandler.Instance.currentStage = Stage.Stage2;//현재 스테이지를 설정
        ColossalHandler.Instance.animanager.SetAnimaction();//현재 스테이 기준으로 애니메이션 다시 설정
        DetectionRange.Instance.SetRecognitionRange();//현재 스테이 기준으로 인식범위 다시 설정
    }
    private void RandomAttack()
    {
        int randomAttack = Random.Range(1, 5);

        switch (randomAttack)
        {
            case 1:
                NodeMachine.Instance.SetNextState("Attack1");
                break;
            case 2:
                NodeMachine.Instance.SetNextState("Attack2");
                break;
            case 3:
                NodeMachine.Instance.SetNextState("Attack3");
                break;
            case 4:
                NodeMachine.Instance.SetNextState("Attack4");
                break;
        }
    }
}
