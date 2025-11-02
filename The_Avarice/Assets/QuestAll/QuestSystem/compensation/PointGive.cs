using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuestSystem
{
    [CreateAssetMenu(menuName = "Quest/compensation/PointGive", fileName = "PointGive")]
    public class PointGive : compensation
    {
        public override void Give(global::QuestSystem.Quest quest)
        {
            Debug.Log("퀘스트 완료 보상 지급");
        }
    }

}
