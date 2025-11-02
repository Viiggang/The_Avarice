using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
 
using System;
namespace QuestSystem
{
    public class QuestSystem : MonoBehaviour
    {

        #region 테스트용 공간 다 지워도 되는 부분

        public TestQuestManager testmanger;
        public GameObject nullQuestObject;
        #endregion
        
        [SerializeField] private Quest[] quests;
        #region 이벤트

        //퀘스트가 끝나면 실행되는 것들
        public delegate void EndQuestScreen(Quest quest);
        public event EndQuestScreen OnQuestUIButtonClear;

        //퀘스트가 들어오면 실행되는 것들
        public delegate void genrateQuest(Quest quest);
        public event genrateQuest OnGenerateQuest;

        #endregion
        public static QuestSystem instance;
       
        public Queue<Quest> deleteQuest = new Queue<Quest>();
        public List<Quest> runtimeQuests = new List<Quest>();
    
        private void Start()
        {
            if (instance == null)
                instance = this;

            QuestPush(quests[0]);

            quests=quests.Skip(1).ToArray();
            #region 테스트 영역
            testmanger.SetData(quests);
            #endregion
        }

        public void removeQuest(Quest endquest,Queue<Quest> deleteQueue)
        {
            //퀘스트 끝나면 내부 클래스 참조 끊는다.
            endquest.End(endquest);

            //List에서 지움
            runtimeQuests.RemoveAll(x => x == endquest);

            bool flag = runtimeQuests.Count > 0 ? false : true;
            nullQuestObject.SetActive(flag);
            //안전 빵으로 null처리
            endquest = null;

            //테스트용
            var QuestData = testmanger.giveQuest();
            if (QuestData == null) return;
            QuestPush(QuestData);
        }

        public void EndQuest(Quest endQuest)
        {
            deleteQuest.Enqueue(endQuest);
            OnQuestUIButtonClear?.Invoke(endQuest);

         
        }

        public void QuestPush(Quest newQuest)
        {  
            var Dta=Instantiate(newQuest);
            Dta.Task = Instantiate(Dta.Task);
            //퀘스트가 생성되면 가장먼저 실행되어야하는것 
            //QuestManager에서 이벤트 등록
            OnGenerateQuest?.Invoke(Dta);

            //퀘스트를 시작한다
            Dta.Run();

            runtimeQuests?.Add(Dta);//퀘스트 등록

            bool flag = runtimeQuests.Count > 0 ? false : true;
            nullQuestObject.SetActive(flag);
        }

    

        public void LoadQuest()
        {
            //퀘스트 들을 가지고 있는 퀘스트 라이브러리 클래스 접근한다
            //퀘스트  코드네임이랑 비교 후 같은 게 있다면 
            //QuestPush 로 넣기
        }

        public void SaveQuest()
        {
            //필요한 저장에 것들
            //퀘스트 코드네임
            //퀘스트 성공 횟수
            
        }
    }

  
}

