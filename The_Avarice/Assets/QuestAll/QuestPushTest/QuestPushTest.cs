using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuestSystem;
public class QuestPushTest : MonoBehaviour
{
    [SerializeField] private Quest quest;

    public void  GiveQuest()
    {
        QuestSystem.QuestSystem.instance.QuestPush(quest);
    }

}
