using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;
using static UnityEditor.PlayerSettings;
namespace ElectricSphere
{
    [CreateNodeMenu("ElectricSphere/Idle ")]
    public class ElectricIdle : BaseState
	{
       private const float DelayTime = 4;
        private float m_time = 0;
        private const float initTime = 0;
        [Input] public BaseState input;
        [Output] public BaseState attack1;
        [Output] public BaseState Attack2;
        [Output] public BaseState death;


        private Transform player;
        private Transform me;
        public Vector3 pos; 
        public override void Enter()
        {
            pos = new Vector3(0, 3f, 0);
            player = ElectHandler.Instance.TransfromManager.Player;
            me = ElectHandler.Instance.TransfromManager.Transform;
        }
        public override void Excute()//4초마다 판단 해서 공격 로직 구현
        {

            if ((m_time >= DelayTime))
            {
                Debug.Log("true");
                //m_time = initTime;
                //if (ColossalHandler.Instance.currentStage == Stage.Stage1)
                //{
                //    ElectricSphereMachine.Instance.SetNextState("attack1");
                //}
                //else if(ColossalHandler.Instance.currentStage == Stage.Stage2)
                //{
                //    ElectricSphereMachine.Instance.SetNextState("attack2");
                //}
                //else
                //{
                //    ElectricSphereMachine.Instance.SetNextState("death");
                //}
                ElectricSphereMachine.Instance.SetNextState("death");
            }
            else
            {
                //추격코드 넣기
                Debug.Log("false");
                me.transform.position = player.transform.position+pos;
                m_time += Time.deltaTime;
            }
        }

        public override void Exit()
        {
            m_time = initTime;
        }
    }
}