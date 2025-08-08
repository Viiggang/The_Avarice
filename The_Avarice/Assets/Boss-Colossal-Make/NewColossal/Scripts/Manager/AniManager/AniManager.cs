using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Colossal;
public class AniManager : MonoBehaviour
{
    public Animator animator;
    public Dictionary<int, IAnimaction> Actions;
    bool isPage1 = true;
    public void Page1()
    {
        if (!isPage1) return;

        Actions = new Dictionary<int, IAnimaction>()
        {
            {1 ,new SpinAttack(animator) },
            {2 ,new blowingAttack(animator) },
            {3 ,new slamDownAttack(animator)},

        };
        isPage1 = false;
    }
    public void Page2()
    {
        if (isPage1) return;
        Actions = new Dictionary<int, IAnimaction>()
        {
            {1 ,new blowingAttack(animator) },
            {2 ,new slamDownAttack(animator)},
            {3 ,new purgeShotAttack(animator) },
            {4 ,new  purgeCannonAttack(animator)},

        };
        isPage1 = true;
    }
}
