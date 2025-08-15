using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarAniManager : MonoBehaviour
{
    public Animator aniManager;
    [Leein.InspectorName("공격")][SerializeField]private MonserAniData Attack;
    [Leein.InspectorName("대기")][SerializeField]private MonserAniData Idle;
    [Leein.InspectorName("움직임")][SerializeField]private MonserAniData Move;
    [Leein.InspectorName("죽음")][SerializeField]private MonserAniData death;
  
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
