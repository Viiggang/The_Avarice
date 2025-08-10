using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Colossal;
public class AniManager : MonoBehaviour
{
    public Animator animator;
    public Dictionary<string, IAnimaction> Actions;
    bool isPage1 = true;
   
    public void SetAnimaction()
    {
        if(ColossalHandler.Instance.currentStage ==Stage.Stage1)
        {
            Page1();
            return;
        }
        else if(ColossalHandler.Instance.currentStage == Stage.Stage2)
        {
            Page2();
            return;  
        }
            Debug.Log("오류 발생 :AniManager 확인 바람 ");
    }
    private void Page1()
    {
        if (!isPage1) return;

        Actions = new Dictionary<string, IAnimaction>()
        {
            {"spin" ,new SpinAttack(animator) },
            {"blowing" ,new blowingAttack(animator) },
            {"slamDown" ,new slamDownAttack(animator)},

        };
        isPage1 = false;
    }
    private void Page2()
    {
        if (isPage1) return;
        Actions = new Dictionary<string, IAnimaction>()
        {
            {"blowing" ,new blowingAttack(animator) },
            {"slamDown",new slamDownAttack(animator)},
            {"purgeShot" ,new purgeShotAttack(animator) },
            {"purgeCannon" ,new  purgeCannonAttack(animator)},

        };
        isPage1 = true;
    }
}
