using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonEvents : MonoBehaviour
{
    [SerializeField]private GameObject OptionUI;
    public void GameStart()
    {
        SceneLoader.Instance.LoadScene("CharatorChoice");
    }
    public void OptionActive()
    { 
        OptionUI?.SetActive(true);
    }
    public void OptionHide()
    {
        OptionUI?.SetActive(false);
    }
    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // 에디터에서 실행 중지
#endif

        Application.Quit();
    }

    public void CharacterChoice()
    {
        SceneLoader.Instance.LoadScene("VillageScene");
    }
}
