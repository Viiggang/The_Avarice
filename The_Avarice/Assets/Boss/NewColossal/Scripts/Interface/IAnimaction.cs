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
namespace Colossal
{

    public class blowingAttack : IAnimaction
    {
        public Animator Animation { get; set; }
        public blowingAttack(Animator Animation)
        {
            this.Animation = Animation;
        }
        public void Excute()
        {
            Animation.SetTrigger("blowing");
        }
    }

    public class slamDownAttack : IAnimaction
    {
        public Animator Animation { get; set; }
        public slamDownAttack(Animator Animation)
        {
            this.Animation = Animation;
        }
        public void Excute()
        {
            Animation.SetTrigger("slamDown");
        }
    }
    public class purgeShotAttack : IAnimaction
    {
        public Animator Animation { get; set; }
        public purgeShotAttack(Animator Animation)
        {
            this.Animation = Animation;
        }
        public void Excute()
        {
            Animation.SetTrigger("purgeShot");
        }
    }
    public class purgeCannonAttack : IAnimaction
    {
        public Animator Animation { get; set; }
        public purgeCannonAttack(Animator Animation)
        {
            this.Animation = Animation;
        }
        public void Excute()
        {
            Animation.SetTrigger("purgeCannon");
        }
    }
    public class SpinAttack :IAnimaction
    {
        public Animator Animation { get; set; }
        public SpinAttack(Animator Animation)
        {
            this.Animation = Animation;
        }
        public void Excute()
        {
            Animation.SetTrigger("SpinAttack");
        }
    }
}