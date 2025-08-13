using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarAniManager : MonoBehaviour
{
    public Animator aniManager;
    [Leein.InspectorName("공격")][SerializeField]private WbAniData Attack;
    [Leein.InspectorName("대기")][SerializeField]private WbAniData Idle;
    [Leein.InspectorName("움직임")][SerializeField]private WbAniData Move;
    [Leein.InspectorName("죽음")][SerializeField]private WbAniData death;
  
    public void Play_Idle()
    {
        Idle.Play(aniManager);
    }
    public void Play_Move()
    {
        Move.Play(aniManager);
    }
    public void Play_Attack()
    {
        Attack.Play(aniManager);
    }
    public void Play_Death()
    {
        death.Play(aniManager);
    }
}
