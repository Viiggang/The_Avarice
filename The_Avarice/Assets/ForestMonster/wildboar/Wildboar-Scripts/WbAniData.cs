using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName =" WildBoarData", menuName = "WbAni/AnimationData")]
public class WbAniData : ScriptableObject
{
    [SerializeField]private string name;
    public void Play(Animator animator)
    {
        animator.SetTrigger(name);
    }

}
