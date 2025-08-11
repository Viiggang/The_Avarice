using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WildBoarManager : MonoBehaviour
{
    private MonsterMachine MonsterMachine;
    void Start()
    {
        MonsterMachine= new MonsterMachine();
        MonsterMachine.ChangeState(new WildBoarIdle());
    }

    // Update is called once per frame
    void Update()
    {
        MonsterMachine.Update();
    }
}
