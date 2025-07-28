using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetAttack : StateMachineBehaviour
{
    [SerializeField]
    string TriggerName;

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger(TriggerName); 
    }
}
