using QuestSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestButton : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI QuestText;
    [SerializeField]private Button Button;
    private Quest quest;

    public void OnEnable()
    {
        if (quest == null) return;
        SetText(quest.Displayname);
    }

    
    public Quest Quest
    {
        get { return this.quest; }
        set
        {
            this.quest = value;
            SetText(quest.Displayname);
            init();
        }
    }
    #region
  
    public delegate void OnclickButton();
    public event OnclickButton OnClick;
    #endregion
    public void init( ) => Button.onClick.AddListener(()=> OnClick?.Invoke());
   
    public void SetText(string text) => QuestText.text = text;
   
   
    public void SetQuest(Quest quest_) => Quest = quest_;

}
