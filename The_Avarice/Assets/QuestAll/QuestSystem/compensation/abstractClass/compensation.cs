using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace QuestSystem
{
    public abstract class compensation : ScriptableObject
    {
        public abstract void Give(global::QuestSystem.Quest quest);
    }
}

