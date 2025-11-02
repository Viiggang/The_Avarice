using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace QuestSystem
{
    public class Target : MonoBehaviour
    {
        public Category Category;
        public string targetname = "ABC";
        public void execute()
        {
            var data = QuestSystem.instance.runtimeQuests;
            foreach (var item in data)
            {
                item.QuestTartgetEqual(Category, targetname);
            }

            bool hasDeleteQuest = QuestSystem.instance.deleteQuest.Count > 0;
            var QuestQueue = QuestSystem.instance.deleteQuest;
            if (hasDeleteQuest)
            {
                foreach (var item in QuestQueue)
                {
                    QuestSystem.instance.removeQuest(item, QuestQueue);

                }

                QuestQueue.Dequeue();
            }
        }
    }

}

