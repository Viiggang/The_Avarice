using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseExecute : ScriptableObject
{
    public virtual void Execute()
    {

    }
}

public class BaseAniEvent : BaseExecute
{
    public string trigger;
    public virtual void Execute(List<GameObject> bulletList)//ÃÑ¾Ë ¹ß»ç
    {

    }
    public virtual void Execute(MonsterController controller)
    {
      
    }
} 
 
       
    
 

