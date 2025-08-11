using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateNodeMenu("Colossal/pattern1")]
public class pattern1 :BaseState
{
    [Input] public BaseState input;
    [Output] public BaseState Attack1;
    [Output] public BaseState Attack2;
    [Output] public BaseState Attack3;
    int randomAttack;
    public override void Enter()
    {
        Debug.Log("pattern1 enter");
        SetStage();
        RandomAttack();
    }

    

    public override void Excute()
    {
        Debug.Log("pattern1 Excute");

    }
    public override void Exit()
    {
        Debug.Log("pattern1 Exit");
    }

    private void SetStage()
    {
        if (ColossalHandler.Instance.currentStage == Stage.Stage1) return;
        ColossalHandler.Instance.currentStage = Stage.Stage1;
        ColossalHandler.Instance.animanager.SetAnimaction();
        DetectionRange.Instance.SetRecognitionRange();
    }
    private void RandomAttack()
    {
        randomAttack = Random.Range(1, 4);
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
        }
    }
}
