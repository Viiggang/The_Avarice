using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherManager : MonoBehaviour
{
    private MonsterMachine monsterMachine;
    // Start is called before the first frame update
    void Start()
    {
        monsterMachine = new MonsterMachine();
        monsterMachine.ChangeState(new ArcherIdle());
    }

    // Update is called once per frame
    void Update()
    {
        monsterMachine.Update();
    }
}
