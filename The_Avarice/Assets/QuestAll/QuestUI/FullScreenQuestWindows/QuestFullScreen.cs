using QuestSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestFullScreen : MonoBehaviour
{
   public Queue<Quest> quests = new Queue<Quest>();

    public GameObject Parent;
    public GameObject instanceButton;

    public descripctionUpdate testUI;

    public List<GameObject> ButtonGameObjects=new List<GameObject>();
    public Queue<GameObject>DeleteGameObjects=new Queue<GameObject>();
 

    public void CreateNewButton(Quest quest)
    {
        var Button = InstantiateButton();
        var QuestButton= Button.GetComponent<QuestButton>() ;

        testUI.Init(quest);
        QuestButton.SetQuest(quest);//

        QuestButton.OnClick += testUI.questUI.Update; 
    }
     
    public GameObject InstantiateButton()
    {
        var NewButton = Instantiate(instanceButton);
        NewButton.transform.SetParent(Parent.transform);
        ButtonGameObjects.Add(NewButton);
        return NewButton;
    }

    public void RemoveQuestButton(Quest quest)
    {
        foreach(var Button in ButtonGameObjects)
        {
            var Data=Button.GetComponent<QuestButton>();
            if (Data == null) continue;

            bool endQuest = Data.Quest.CodeName == quest.CodeName;
            if (endQuest)
            {
                DeleteGameObjects.Enqueue(Button);
                Destroy(Button.gameObject);
            }     
        }

 
        ButtonGameObjects.RemoveAll(x =>  DeleteGameObjects.Contains(x));
        DeleteGameObjects.Clear();
    }
}
