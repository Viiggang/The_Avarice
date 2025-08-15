using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "patrol", menuName = "ArcherStates/Patrol")]
public class ArcherPatrol : MonsterStates
{
    private Transform findPlayer;
    private float movespeed;
    private ArcherManager manager;
    [Leein.InspectorName("실행할 애니메이션 이름")][SerializeField]private string PatrolPlay;
    public Dictionary<string, MonsterStates> StatusList;
    public override void Enter(ArcherManager manager)
    {
        this.manager = manager;
        StatusList= manager.StatusList;
        this.manager.ArcherAni.Play(PatrolPlay);
        Debug.Log("ArcherPatrol_Enter");
    }
    public override void Update()
    {

    }
    public override void Exit()
    {

    }
}