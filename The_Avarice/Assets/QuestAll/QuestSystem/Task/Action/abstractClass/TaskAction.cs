using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace QuestSystem
{

    public abstract class TaskAction : ScriptableObject
    {

        public abstract int Run(global::QuestSystem.Task Task, int CurrentSuccessCount, int successCount);
    }
}
