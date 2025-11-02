using QuestSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[CreateAssetMenu(menuName ="UpdateUI/Update/descripction",fileName = "descripction_")]
public class descripctionData :UpdateUI
{
    private TextMeshProUGUI TextMeshPro;
    private Quest quest;
    public override void init(object data, params object[] objects)
    {
         if(data is TextMeshProUGUI value)
            TextMeshPro=value;

        foreach(object obj in objects)
        {
            if (obj is Quest quest_)
                quest = quest_;
        }
    }

    public override void Update()
    {
        TextMeshPro.text = $"{quest.Description}";
    }
}
