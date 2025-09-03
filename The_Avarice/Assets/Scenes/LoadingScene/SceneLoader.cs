using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{
    public string nextSceneName { get; private set; }

    private static SceneLoader _instance;
    public static SceneLoader Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject sceneLoader = new GameObject("SceneLoader");
                _instance = sceneLoader.AddComponent<SceneLoader>();
                DontDestroyOnLoad(sceneLoader);
            }
            return _instance;
        }
    }

    private void Start()
    {
        StartCoroutine(LoadSceneCoroutine());
    }

    public void LoadScene(string sceneName)
    {
        yield return FadeInOut.Instance.FadeOut;
        nextSceneName = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }

    private IEnumerator LoadSceneCoroutine()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(nextSceneName);
        while (!asyncLoad.isDone)
            yield return null;

        yield return FadeInOut.Instance.FadeIn();
    }
}

