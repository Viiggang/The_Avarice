using UnityEngine.SceneManagement;
using UnityEngine;
namespace Leein
{
    public static class ButoonFun
    {
        static public void Gamestart()
        {
            Debug.Log("START");
            SceneManager.LoadScene("Character selection");
        }

        static public void GameExit()
        {
            Debug.Log("EXIT");

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; // 에디터에서 실행 중지
#endif

            Application.Quit();
        }

        static public void ShowGameoption()
        {
            Debug.Log("OPTION");
        }
    }
}

