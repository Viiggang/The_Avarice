using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting.Antlr3.Runtime.Collections;
using UnityEngine;

public class ArcherManager : MonoBehaviour
{
     public ArcherAni ArcherAni;
    public MonsterMachine2 monsterMachine;
    public Dictionary<string, MonsterStates> StatusList;
    [SerializeField] private ArcherStatus ArcherStatus;
    [SerializeField] private List<MonsterStates> States;
    [Leein.InspectorName("시작 상태")][SerializeField] private string Currentstate;
    private void Awake()
    {
        monsterMachine = new MonsterMachine2(this);
        StatusList = States.ToDictionary(s => s.Name, s => s);
        monsterMachine.ChangeState(StatusList[Currentstate]);
    }
  
    // Update is called once per frame
    void Update()
    {
        monsterMachine.Update();
    }
}
