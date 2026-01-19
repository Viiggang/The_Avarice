 
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
            if (Input.anyKeyDown == false || CurrentQuest == null)
                return;
            InputAction();
        }
        public void InputAction()
        {
            Debug.Log("½ÇÇà");
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
            {
                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    target = "Dash";
                    execute();
                    IsCompleted();
                }
                else
                {
                    target = "move";
                    execute();
                    IsCompleted();
                }
            }
           
            else if(Input.GetKeyDown(KeyCode.F))
            {
        
                target = "inspection";
                execute();
                IsCompleted();
            }
            else if (Input.GetKeyDown(KeyCode.B))
            {
                //
                target = "Inventory";
                execute();
                IsCompleted();
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
               
                target = "Skill1";
                execute();
                IsCompleted();
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
             
                target = "Skill2";
                execute();
                IsCompleted();
            }
            else if (Input.GetKeyDown(KeyCode.C))
            {
                
                target = "Attack";
                execute();
                IsCompleted();
            }
            //////////////////



        }

        private void IsCompleted()
        {
            if (CurrentQuest.QuestState == QuestState.Complete)
            {
                CurrentQuest = null;
                Category = null;
                target = null;
            }
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
