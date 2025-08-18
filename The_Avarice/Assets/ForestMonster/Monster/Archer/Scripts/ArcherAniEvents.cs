using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherAniEvents : MonoBehaviour
{
    public GameObject bullet;
    public MonsterController controller;
    public void Attacktoidle()
    {
        controller.MonsterMachine.ChangeState(controller.State["idle"], controller);
    }
    public void Attack()
    {
        Instantiate(bullet, controller.MonsterTrans.position, controller.MonsterTrans.rotation);
    }
}
