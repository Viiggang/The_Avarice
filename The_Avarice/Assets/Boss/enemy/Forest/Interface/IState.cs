using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IState<T>
{

    public void Enter(T controller);
    public void Excute(T controller);
    public void Exit(T controller);

}
 