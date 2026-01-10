using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMachine<T>
{
    private IState<T> currentState;
    T controller;
    public MonsterMachine(T controller)
    {
        this.controller = controller;
    }
    public void ChangeState(baseStates<T> newState, T controller)
    {
        currentState?.Exit(controller);
        currentState = newState;
        currentState.Enter(controller);
    }

    public void Update()
    {
        currentState?.Excute(controller);
    }

}