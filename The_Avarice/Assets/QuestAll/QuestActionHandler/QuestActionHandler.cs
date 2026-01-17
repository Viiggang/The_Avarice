 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace QuestSystem
{
    public class QuestActionHandler : MonoBehaviour
    {
        public Category Category;
        public string target;

        public Quest CurrentQuest;

        void Update()
        {
            InputAction();
        }
        public void InputAction()
        {
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            {
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    target = "ABC";
                    execute();
                    if (CurrentQuest.QuestState == QuestState.Complete)
                    {
                        CurrentQuest = null;
                        Category = null;
                        target = null;
                    }
                }
            }
            //////////////////
            


        }
        public void SetQuest(Quest Data)
        {
            Category = Data.GetCategory();
            CurrentQuest = Data;
        }

        public void execute()
        {

            var data = QuestSystem.instance.runtimeQuests;
            foreach (var item in data)
            {
                item.QuestTartgetEqual(Category, target);
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
