using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace ElectricSphere
{
    [CreateNodeMenu("ElectricSphere/ElectricAttack2")]
    public class ElectricAttack2 : BaseState
	{
        [Input] public BaseState input; private ElectAniManager manager;
        public override void Enter()
        {
            manager = ElectHandler.Instance.ElectAniManager;
            if (manager != null)
            {
                manager.Manager.SetTrigger("Attack2");
            }
        }
        public override void Excute()
        {

        }

        public override void Exit()
        {

        }
    }
}