using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace ElectricSphere
{
    [CreateNodeMenu("ElectricSphere/ElectricAttack1")]
    public class ElectricAttack1 : BaseState
	{
        [Input] public BaseState input;
        private ElectAniManager manager;
        public override void Enter()
        {
            manager = ElectHandler.Instance.ElectAniManager;
            if(manager !=null)
            {
                manager.Manager.SetTrigger("Attack1");
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