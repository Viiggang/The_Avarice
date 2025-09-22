using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonEvents : MonoBehaviour
{
    [SerializeField] private GameObject OptionUI;
    public void GameStart()
    {
        SceneManager.LoadScene("CharatorChoice"); // 로딩 안함
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

    public void PaladinChoice()
    {
        PlayerMgr.instance.setPlayerType(Player_Type.Paladin);
        SceneLoader.Instance.ChangeScene("VillageScene");
    }
    public void IgnisChoice()
    {
        PlayerMgr.instance.setPlayerType(Player_Type.Ignis);
        SceneLoader.Instance.ChangeScene("VillageScene");
    }
    public void WindBreakerChoice()
    {
        PlayerMgr.instance.setPlayerType(Player_Type.WindBreaker);
        SceneLoader.Instance.ChangeScene("VillageScene");
    }
    public void SoulEaterChoice()
    {
        PlayerMgr.instance.setPlayerType(Player_Type.SoulEater);
        SceneLoader.Instance.ChangeScene("VillageScene");
    }
}
