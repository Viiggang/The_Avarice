using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAnimactionEvents : MonoBehaviour
{
    public void NextToIdle()
    {
        NodeMachine.Instance.SetNextState("next");
    }
    public void SpinAttack()
    {

    }
}
