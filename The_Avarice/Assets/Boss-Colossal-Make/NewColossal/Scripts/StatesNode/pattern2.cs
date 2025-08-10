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
        //�̹� �ѹ� ���� ������ ���ư�
        if (ColossalHandler.Instance.currentStage == Stage.Stage2) return; 
        ColossalHandler.Instance.currentStage = Stage.Stage2;//���� ���������� ����
        ColossalHandler.Instance.animanager.SetAnimaction();//���� ������ �������� �ִϸ��̼� �ٽ� ����
        DetectionRange.Instance.SetRecognitionRange();//���� ������ �������� �νĹ��� �ٽ� ����
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
