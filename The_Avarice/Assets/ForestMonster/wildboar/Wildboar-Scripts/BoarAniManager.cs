using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarAniManager : MonoBehaviour
{
    public Animator aniManager;
    private string playname;
    public void Play_Idle()
    {
        playname = "Idle";
        aniManager.SetTrigger(playname);
    }
    public void Play_Move()
    {
        playname = "move";
        aniManager.SetTrigger(playname);
    }
    public void Play_Attack()
    {
        playname = "Attack";
        aniManager.SetTrigger(playname);
    }
    public void Play_Death()
    {
        playname = "death";
        aniManager.SetTrigger(playname);
    }
}
