using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepState :IBossState
{
    public void Enter(BossController boss)
    {
       
        Vector2 m_size=new Vector2(0.6997521f, 0.3200178f);

        boss.Collider.size= m_size;

    }
    public void Execute(BossController boss)
    {

    }

    public void Exit(BossController boss)
    {


    }
}
