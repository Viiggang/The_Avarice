using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuestSystem;
using static QuestSystem.QuestSystem;
using static UnityEditor.Progress;
public class QuestManager : MonoBehaviour
{
    [SerializeField] private QuestSystem.QuestSystem questSystem;
    [SerializeField] private testUI QuestWindow;
     

    [SerializeField] private QuestFullScreen questscreen;
    [SerializeField] private GameObject parent;
    [SerializeField] private GameObject obj;
    void Awake()
    {
        RegisterQuestEvents();
       
    }

     public void RegisterQuestEvents()
     {
       
        questSystem.OnGenerateQuest += NewQuestAction;//
        questSystem.OnQuestUIButtonClear += questscreen.RemoveQuestButton;
        questSystem.OnGenerateQuest += questscreen.CreateNewButton;
        
    }

 

    public void NewQuestAction(Quest quest)
    {
     

        var Data = Instantiate(obj);//생성하고
        Data.transform.SetParent(parent.transform);//부모 밑으로
        Data.transform.localScale = new Vector3(1f, 1f, 1f);
        var data2 = Data.GetComponent<TestUpddate>();

        data2.Data.init(data2.TextMeshProUGUI, quest, data2.gameObject);//초기화

        quest.Task.onchangeSuccessCount += data2.Data.Update;//성공 횟수 증가할 때 작동
        quest.OnStartQuestEvent += data2.Data.Update;//퀘스트 Run일 때 작동 
        quest.OnEndQuestEvent += data2.Data.DestroyGameObject;//퀘스트 끝났을 때 작동
        
    }
}
