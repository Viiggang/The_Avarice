using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SceneLoader : MonoBehaviour
{
    public string nextSceneName { get; private set; }

    public static SceneLoader Instance { get {  return _instance; } }
    private static SceneLoader _instance;

    public Image fadeImage;
    public float fadeDuration = 1f;

    public Animator loadingAnimator;

    private void Awake()
    {
        if(_instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void ChangeScene(string sceneName)
    {
        nextSceneName = sceneName;

        fadeImage.DOFade(1, fadeDuration).OnStart(() =>
        {
            fadeImage.raycastTarget = true;
        })
            .OnComplete(() =>
        {
            SceneManager.LoadScene("LoadingScene");
            StartCoroutine("LoadScene");
        });
    }

    public IEnumerator LoadScene()
    {
        AsyncOperation loadscene = SceneManager.LoadSceneAsync(nextSceneName);
        loadscene.allowSceneActivation = false;

        Player_Type type = PlayerMgr.instance.getPlayerType();
        if (type == Player_Type.NULL)
            yield break;

        switch (type)
        {
            case Player_Type.Paladin:

                break;

            case Player_Type.Ignis:

                break;

            case Player_Type.WindBreaker:

                break;

            case Player_Type.SoulEater:

                break;

            default:
                Debug.Log($"LoadingScene type error");
                break;
        }

    }

    //public IEnumerator FadeOut()
    //{
    //    fadeImage.raycastTarget = true;
    //    Color color = fadeImage.color;
    //    for (float t = 0; t < fadeDuration; t += Time.deltaTime)
    //    {
    //        color.a = Mathf.Lerp(0, 1, t / fadeDuration);
    //        fadeImage.color = color;
    //        yield return null;
    //    }
    //    color.a = 1;
    //    fadeImage.color = color;
    //}

    //public IEnumerator FadeIn()
    //{
    //    Color color = fadeImage.color;
    //    for (float t = 0; t < fadeDuration; t += Time.deltaTime)
    //    {
    //        color.a = Mathf.Lerp(1, 0, t / fadeDuration);
    //        fadeImage.color = color;
    //        yield return null;
    //    }
    //    color.a = 0;
    //    fadeImage.color = color;
    //    fadeImage.raycastTarget = false;
    //}
}

