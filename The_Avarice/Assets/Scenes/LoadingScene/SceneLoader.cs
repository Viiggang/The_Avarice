using DG.Tweening;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [InitializeOnLoad]
    public class SceneSettings
    {
        static SceneSettings()
        {
            var pathOfFirstScene = EditorBuildSettings.scenes[0].path;
            var sceneAsset = AssetDatabase.LoadAssetAtPath<SceneAsset>(pathOfFirstScene);
            EditorSceneManager.playModeStartScene = sceneAsset;
        }
        
    }

    public string nextSceneName { get; private set; }

    public static SceneLoader Instance { get {  return _instance; } }
    private static SceneLoader _instance;

    public GameObject fadeImage;
    public GameObject loadingCharacter;
    public GameObject loadingText;

    public float fadeDuration = 1f;

    private Image image;

    private void Awake()
    {
        if(_instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += CompleteSceneLoaded;
    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= CompleteSceneLoaded;
    }

    public void ChangeScene(string sceneName)
    {
        fadeImage.SetActive(true);

        nextSceneName = sceneName;

        image.DOFade(1, fadeDuration).OnStart(() =>
        {
            image.raycastTarget = true;
        })
            .OnComplete(() =>
        {
            SceneManager.LoadScene("LoadingScene");
            StartCoroutine("LoadScene");
        });
    }

    public IEnumerator LoadScene()
    {
        loadingCharacter.SetActive(true);
        loadingText.SetActive(true);

        Player_Type type = PlayerMgr.instance.getPlayerType();
        Animator animator = loadingCharacter.GetComponent<Animator>();
        Text text = loadingText.GetComponent<Text>();

        AsyncOperation loadscene = SceneManager.LoadSceneAsync(nextSceneName);
        loadscene.allowSceneActivation = false;

        float past_time = 0;
        float percentage = 0;

        while (!(loadscene.isDone))
        {
            yield return null;

            animator.SetInteger("type", (int)PlayerMgr.instance.getPlayerType());

            past_time += Time.deltaTime;

            if (percentage >= 90)
            {
                percentage = Mathf.Lerp(percentage, 100, past_time);

                if (percentage == 100)
                {
                    loadscene.allowSceneActivation = true;
                }
            }
            else
            {
                percentage = Mathf.Lerp(percentage, loadscene.progress * 100f, past_time);
                if (percentage >= 90) past_time = 0;
            }
            text.text = percentage.ToString("0") + "%";
        }

    }

    private void CompleteSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        image.DOFade(0, fadeDuration)
        .OnStart(() => {
            fadeImage.SetActive(false);
            loadingCharacter.SetActive(false);
            loadingText.SetActive(false);
        })
        .OnComplete(() => {
            image.raycastTarget = false;
            SceneManager.LoadScene(nextSceneName);
        });
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

