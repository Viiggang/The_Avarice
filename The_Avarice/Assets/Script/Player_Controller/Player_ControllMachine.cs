using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_ControllMachine : MonoBehaviour
{
    private IpController currentState;

    public IpController CurrentState { get; private set; }

    public void Initialize(IpController startState)
    {
        CurrentState = startState;
        CurrentState.Enter();
    }

    public void ChangeState(IpController newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}
