using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName =" monsterAniData", menuName = "MonsterAnimaction/AnimationData")]
public class MonserAniData : ScriptableObject
{
    [SerializeField]public string Playname;
    public void Play(Animator animator)
    {
        animator.SetTrigger(Playname);
    }

}
