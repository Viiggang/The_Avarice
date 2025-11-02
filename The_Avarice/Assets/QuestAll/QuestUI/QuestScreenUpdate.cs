using QuestSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


[CreateAssetMenu(menuName ="Quest/FullScreen/descripctionUpdate",fileName ="descripction_")]
public class QuestScreenUpdate : UpdateUI
{
    public TextMeshProUGUI textMeshPro;
    private Quest quest; 
    public override void init(object data, params object[] objects)
    {
         if(data is TextMeshProUGUI value)
            textMeshPro= value;

         foreach(var item in objects)
         {
            if(item is Quest quest_)
                quest = quest_;
         }
    }

    public override void Update()
    {
        textMeshPro.text = $"";
 
    }
}
