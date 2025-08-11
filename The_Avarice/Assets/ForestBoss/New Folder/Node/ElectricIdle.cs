using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
namespace ElectricSphere
{
    [CreateNodeMenu("ElectricSphere/Idle ")]
    public class ElectricIdle : BaseState
	{
        public float DelayTime = 0;
        private float m_time = 0;
        private const float initTime = 0;
        [Input] public BaseState input;
        [Output] public BaseState attack1;
        [Output] public BaseState Attack2;
        [Output] public BaseState death;
        public override void Enter()
        {

        }
        public override void Excute()//4초마다 판단 해서 공격 로직 구현
        {

        }

        public override void Exit()
        {

        }
    }
}