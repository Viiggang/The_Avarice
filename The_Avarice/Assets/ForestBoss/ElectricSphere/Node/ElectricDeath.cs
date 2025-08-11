using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace ElectricSphere
{
    [CreateNodeMenu("ElectricSphere/Death")]
    public class ElectricDeath : BaseState
	{
        [Input] public BaseState input;
        public override void Enter()
        {
            var manager = ElectHandler.Instance.ElectAniManager.Manager;
            manager.SetTrigger("death");
        }
        public override void Excute()
        {

        }

        public override void Exit()
        {

        }
    }
}