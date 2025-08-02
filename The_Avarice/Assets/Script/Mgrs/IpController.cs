using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IpController
{
    void Enter();         
    void Exit();          
    void HandleInput();   
    void LogicUpdate();   
    void PhysicsUpdate();
}
