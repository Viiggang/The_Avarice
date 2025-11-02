using QuestSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class descripctionUpdate : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public UpdateUI questUI;

  
    private void Start() => this.gameObject.SetActive(false);

    public void Init(Quest quest) => questUI.init(textMeshPro, quest);
 
    public void OnDisable() => textMeshPro.text = "";
   
}
