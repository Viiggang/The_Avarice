using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace QuestSystem
{
    public enum QuestState
    {
        inactive,
        active,
        waitForComplete,
        Complete
    }

    [CreateAssetMenu(menuName = "Quest/CreateQuest", fileName = "Quest_")]
    [System.Serializable]
    public class Quest : ScriptableObject
    {
        /*
         퀘스트가 보통 하는일이 무엇일까?

            퀘스트 내용
            보상

          [SerializeField] private Category Category; //퀘스트 정보
          퀘스트 시스템에서 가지고 있는 카테고리랑
          타겟의 카테고리가 같은지 비교 후  Task의 작업을 실행 할 것이다.


         */
        #region
        public delegate void OnEndQuest(Quest quest);//퀘스트가 완료되면 UI를 파괴한다.
        public event OnEndQuest OnEndQuestEvent;

        public delegate void StartQuest();//시작할 때 UI를 업데이트 한다.
        public event StartQuest OnStartQuestEvent;
        #endregion
        private QuestState questState;
        [SerializeField] private string codename;//코드 내부에서 검색용
        [TextArea][SerializeField] private string description;//설명
        [SerializeField] private string displayname;//표시 내용

        public string CodeName =>this.codename;
        public string Description => this.description;
        public string Displayname => this.displayname;
        public Task Task
        {
            get
            {
                return this.task;
            }
            set 
            {
                this.task = value;
            }
        }

        [SerializeField] private Category Category; //퀘스트 카테고리
        [SerializeField] private Task task;//퀘스트 일
        [SerializeField] private compensation compensation;// 보상


        [Header("자동 완료 (체크O) 활성/(체크X)비활성")]
        [SerializeField] public bool AutoComplete;

        public QuestState QuestState
        {
            get { return this.questState; }
            set
            {
                questState = value;

                bool isQuestCompleted = questState == QuestState.Complete;
                if (isQuestCompleted)
                {
                    Complete(); 
                    OnEndQuestEvent?.Invoke(this);
                }
                     
            }
        }

        public void Run()//실행
        {
            //퀘스트를 활성화
            questState=QuestState.active;
         
            OnStartQuestEvent?.Invoke();
            task.Start();
        }

        public void End(Quest quest)
        {
            task.End();
            task = null;
            Category = null;
            compensation = null;
          
        }

        public void QuestExecute()//성공
        {
            const int SuccessCount = 1;

            task.ReceiveReport(SuccessCount);

            if (task.IsComplete)
            {
                QuestState = AutoComplete ? QuestState.Complete : QuestState.waitForComplete;
            }
               
        }

        public void Complete()//퀘스트 완료
        {
            compensation.Give(this);
            QuestSystem.instance.EndQuest(this);
        }

        public void QuestTartgetEqual(Category category, object tartget)//먼저 타겟인지 판별용
        {
            bool isTargetMatched = task.IsTarget(category.CodeName, tartget);
            if (isTargetMatched)
            {
                QuestExecute();
            }
        }

        //퀘스트 오토 컴플리트 비활성 되면 퀘스트 완료 후 WaitForComplete인데 주체쪽에서 밑에 함수 실행한다.
        public void CompleteCheackState(string codename)
        {
            bool isDifferentCodeName = this.codename != codename;
            if (isDifferentCodeName) return;

            bool canCompleteQuest = questState == QuestState.waitForComplete;
            if (canCompleteQuest)
            {
                questState = QuestState.Complete;
            }
        }
    }

}
