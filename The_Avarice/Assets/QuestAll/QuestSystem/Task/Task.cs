using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using QuestSystem;
namespace QuestSystem
{
    public enum TaskState
    {
        inactive,//비활성
        running,//퀘스트 하는 중
        complete//퀘스트 완료
    }

    [CreateAssetMenu(menuName = "Quest/Task/CreateNewTask", fileName = "Task_")]
    public class Task : ScriptableObject
    {
        #region private 
        public delegate void OnChangeTaskState();
        [SerializeField] private string codename;//나중에 검색의 기능을 추가시 사용
        [TextArea][SerializeField] private string description;//현재 Task가 뭐하는 기능인지 설명용
        [SerializeField] private TaskAction taskAction; //Task가 하는 작업
        [SerializeField] private Category category;
        [SerializeField] private TaskTarget[] tasktarget;//타겟이 여러개 일수도 있음
        [SerializeField] private int successcount = 0;
        [SerializeField] private int questclearcount;//성공 카운트가QuestClearCount 같으면 클리어
        [SerializeField] private TaskState taskstate;
        #endregion

        #region public
        
        public event OnChangeTaskState onchangeSuccessCount;

        public string CodeName => this.codename;

        public string Description => this.description;

        public bool IsComplete => taskstate == TaskState.complete;

        public int QuestClearCount => this.questclearcount;

        public int Successcount
        {
            get
            {
                return successcount;
            }

            set
            {
                successcount = Mathf.Clamp(value,0, QuestClearCount);

                onchangeSuccessCount?.Invoke();

                bool QuestClear = successcount == questclearcount;
                if (QuestClear)
                {
                    Complete();
                }
            }
        }

        public TaskState TaskState
        {
            get => this.taskstate;

            set
            {
                this.taskstate = value;
            }
        }
        #endregion

        public void ReceiveReport(int _SuccessCount) => Successcount = taskAction.Run(this, Successcount, _SuccessCount);

        public void Start() => TaskState = TaskState.running;
       
        public void End()
        {
            onchangeSuccessCount = null;
            tasktarget = null;
            category = null;
            taskAction = null;
          
        }

        private void Complete() => TaskState = TaskState.complete;

        //카테고리 랑 퀘스트 타겟인지 판별
        public bool IsTarget(string category, object target) => this.category == category && this.tasktarget.Any(x => x.IsEqual(target));


    }

}
