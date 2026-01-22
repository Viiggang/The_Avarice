using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMachine<T>
{
    private IState<T> currentState;
    T controller;
    private MonsterStatus m_monsterStatus;
    public MonsterMachine(T controller, MonsterStatus monsterStatus )
    {
        this.controller = controller;
        m_monsterStatus = monsterStatus;
    }
    public void ChangeState(baseStates<T> newState, T controller)
    {
        currentState?.Exit(controller);
        currentState = newState;
        currentState?.Enter(controller);
    }

    public void Update()
    {
        if (m_monsterStatus.isDead) return;
        currentState?.Excute(controller);
    }

}