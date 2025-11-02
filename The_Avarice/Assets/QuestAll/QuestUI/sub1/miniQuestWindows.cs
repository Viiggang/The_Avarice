using QuestSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Test/UpdateMiniQuest", fileName = "test")]
public class miniQuestWindows : UpdateText
{
    private TextMeshProUGUI TextMeshProUGUI;
    private GameObject obj;
    private Quest quest;
    public override void init(object data, params object[] objects)
    {
            if(data is TextMeshProUGUI value)
            TextMeshProUGUI = value;

            foreach(var item in objects)
        {
            if (item is Quest value2)
                quest = value2;

            if(item is GameObject value3)
                obj = value3;
        }
                  

    }

    public override void Update()
    {
        TextMeshProUGUI.text = $"{quest.Displayname}({quest.Task.Successcount}/{quest.Task.QuestClearCount})";
    }

    public override void DestroyGameObject(Quest quest)
    {
        TextMeshProUGUI = null;
        quest = null;
        Destroy(obj);
    }
}
