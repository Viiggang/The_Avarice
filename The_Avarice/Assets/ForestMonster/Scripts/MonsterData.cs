using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MonsterData", menuName = "Data/Monster")]
public class MonsterData : ScriptableObject
{
    [SerializeField] private string monstername;
    [SerializeField] private float hp; // 내부 변수
    [SerializeField] private float damage;
    [SerializeField] private float movespeed;
    [SerializeField] private float patroltime;
    [SerializeField] private float idletime;
    public float Hp
    {
        get { return hp; }
        set{ hp = Mathf.Round(value);}
    }
    public float Damage
    {
        get {return damage;}
       
    }
    public string Monstername
    {
        get { return monstername; }
       
    }
    public float MoveSpeed
    {
        get { return movespeed; }
        
    }
    public float PatrolTime
    {
        get { return patroltime; }
    }
    public float IdleTime
    {
        get { return idletime; }
    }
}
