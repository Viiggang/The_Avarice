using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace QuestSystem
{
    [CreateAssetMenu(menuName = "Quest/Task/TaskAction/PositiveCount", fileName = "TaskAction_PositiveCount")]
    public class PositiveCount : TaskAction
    {
        public override int Run(Task task, int CurrentSuccessCount, int successCount)
        {
            return successCount > 0 ? CurrentSuccessCount + successCount : CurrentSuccessCount;
        }
    }
}
