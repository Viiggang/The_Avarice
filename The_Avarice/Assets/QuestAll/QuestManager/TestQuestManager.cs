using System.Collections;
using System.Collections.Generic;
using QuestSystem;
using Unity.VisualScripting;
using UnityEngine;

public class TestQuestManager : MonoBehaviour
{
  
    [SerializeField] private Queue<Quest> questQueue = new Queue<Quest>(); 

    

    public void SetData(Quest[] questList)
    {
        foreach(Quest quest in questList)
        {
            questQueue.Enqueue(quest);
        }
    }

    public Quest giveQuest()
    {
        if (!(questQueue.Count > 0)) return null;
        return questQueue.Dequeue();
    }
}
