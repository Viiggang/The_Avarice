//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//namespace ElectricSphere
//{
//    public class SphereHandler : Singleton<SphereHandler>
//    {
//        public EmAnimaction animanager;
//        public EmTransform EmTransform;
//        public Animator SphereManager;
//        // Start is called before the first frame update

//        private float delay=4;
//        private float m_time;
//        private void Awake()
//        {
//            base.Awake();
//        }
       
//        void Update()
//        {
//            if(m_time>delay)
//            {
//                m_time = 0;
//                EmTransform.chase = false;
//                var flag = ColossalHandler.Instance != null && ColossalHandler.Instance.currentStage == Stage.Stage1;
//                if (ColossalHandler.Instance !=null&& ColossalHandler.Instance.currentStage == Stage.Stage1) //���� �������� ���� �ٸ� ���� ����  ������ ������ ���ⱸü ������ ������
//                {
//                    animanager.Attackani.SetTrigger("Attack1");
//                }
//                else if (ColossalHandler.Instance != null&& ColossalHandler.Instance.currentStage == Stage.Stage2)
//                {
//                    animanager.Attackani.SetTrigger("Attack2");
//                }
//                else 
//                {
//                    animanager.Attackani.SetTrigger("Attack1");
//                }
//            }
//            else
//            {
//                m_time += Time.deltaTime;
//            }
//        }
//    }

//}
