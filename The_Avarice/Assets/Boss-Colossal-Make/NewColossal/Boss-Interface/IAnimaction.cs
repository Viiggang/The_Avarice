using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAnimaction 
{
    public Animator Animation { get; set; }
    public void Excute()
    {

    }
}

public class Attack1: IAnimaction
{
    public Animator Animation { get; set; }
    public Attack1(Animator Animation)
    {
        this.Animation = Animation;
    }
    public void Excute()
    {
        Animation.SetTrigger("Attack");
    }
}

public class Attack2 : IAnimaction
{
    public Animator Animation { get; set; }
    public Attack2(Animator Animation)
    {
        this.Animation = Animation;
    }
    public void Excute()
    {
        Animation.SetTrigger("Attack2");
    }
}