using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AniManager : MonoBehaviour
{
    public Animator animator;
    public Dictionary<string, IAnimaction> Actions;

    private void Awake()
    {
        Actions = new Dictionary<string, IAnimaction>()
        {
            { "Attack" ,new Attack1(animator) },
        };

    }
}
