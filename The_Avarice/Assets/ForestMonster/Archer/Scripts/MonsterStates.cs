using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
 
public abstract class MonsterStates : ScriptableObject
{
    public string Name;
    public abstract void Enter(ArcherManager Manager);
    public abstract void Update();
    public abstract void Exit();
 
}
