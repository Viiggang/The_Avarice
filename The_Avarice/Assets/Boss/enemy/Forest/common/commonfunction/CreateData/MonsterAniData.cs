using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName =" monsterAniData", menuName = "MonsterAnimaction/AnimationData")]
public class MonsterAniData : ScriptableObject
{
    [SerializeField]public string Playname;
    public void Play(Animator animator)
    {
        animator.SetTrigger(Playname);
    }

}
