using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectWindowController : MonoBehaviour
{
    string myName;
    Button SelfButton;
    public Text SelectionText;

    public static class ButtonInfo
    {
        public const string YesButtonText = "Yes";
        public const string NoButtonText = "No";
        public const string JobText = "JobText";
    }

    private void Awake()
    {
        myName = this.gameObject.name;
        SelfButton = GetComponent<Button>();

    }
    void Start()
    {
      
        switch (myName)
        {
            case "Yes":
                SelfButton.onClick.AddListener(NextStage);
                break;
            case "No":
                SelfButton.onClick.AddListener(ActiveFalse);
                break;
            case "Job_Text":
               
                SelectionText = GetComponent<Text>();
                SelectionText.text = CharacterSelectionController.Instance.CharacterSelection.ToString();
                break;
            case "Button":
                SelfButton.onClick.AddListener(ActiveTrue);
                break;
            default:
               
                Debug.Log("�̸� Ȯ�� ��");
                break;
        }

        

    }
    void NextStage()//<-- ���������� �̵� ��
    {
        Debug.Log("������������ ��");
    }
    void ActiveFalse()
    {
        var obj=GameObject.Find("YesOrNo_UI");
            obj.SetActive(false);
    }
    void ActiveTrue()
    {
        GameObject[] allObjects = Resources.FindObjectsOfTypeAll<GameObject>();
        foreach (var obj in allObjects)
        {
            if (obj.name == "YesOrNo_UI")
            {
                obj.SetActive(true);
                var obT=GameObject.Find("Job_Text");
                SelectionText= obT.GetComponent<Text>();
                SelectionText.text = CharacterSelectionController.Instance.CharacterSelection.ToString();
               // Debug.Log("������Ʈ�� Ȱ��ȭ�߽��ϴ�!");
            }
        }
        
    }

}
