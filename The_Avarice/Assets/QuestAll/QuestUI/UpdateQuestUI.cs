using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
namespace QuestSystem
{
    [CreateAssetMenu(menuName = "UpdateUI/Update/QuestUI", fileName = "QuestUI_")]
    public class UpdateQuestUI : UpdateUI
    {
        [SerializeField] private TextMeshProUGUI QuestText;

        public override void init(object data, object[] objects)
        {
            if (data is  TextMeshProUGUI value)
                     QuestText = value;
        }

        public override void Update()
        {
            var data = QuestSystem.instance;
            QuestText.text = "";
           
            bool hasQuest = data.runtimeQuests.Count > 0;
            if (hasQuest)
            {
                foreach (var item in data.runtimeQuests)
                {
                    QuestText.text += $"{item.Displayname}:{item.Task.Successcount}/{item.Task.QuestClearCount}\n";
                }
            }
        }

        
    }

}

