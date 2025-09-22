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
    [SerializeField] private float attackdistance;
    [SerializeField] private int defense;
    public float AttackDistance
    {
        get { return attackdistance; }
        set { attackdistance = value; }
    }
    public float Hp
    {
        get { return hp; }
        set { hp = Mathf.Round(value); }
    }
    public float Damage
    {
        get { return damage; }
        set { damage = Mathf.Round(value); }
    }
    public string Monstername
    {
        get { return monstername; }
        set { monstername = value; }
    }
    public float MoveSpeed
    {
        get { return movespeed; }
        set { movespeed = value; }
    }
    public float PatrolTime
    {
        get { return patroltime; }
        set { patroltime = value; }
    }
    public float IdleTime
    {
        get { return idletime; }
        set { idletime = value; }
    }
    public int Defense
    {
        get { return defense; }
        set { defense = value; }
    }
}
