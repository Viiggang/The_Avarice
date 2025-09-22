using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

public  class BaseState<T> : Node 
{
    [HideInInspector]public T Instance;
   
    // Use this for initialization
    protected override void Init()
    {
		base.Init();
		
	}
    public void SetInstance(T data)
    {
        Instance=data;
    }



    public virtual void Enter(T Data)
    {

    }
    public virtual void Excute(T Data)
    {

    }
    public virtual void Exit(T Data)
    {

    }
}