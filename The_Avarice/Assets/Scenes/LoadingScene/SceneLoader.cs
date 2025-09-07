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

    public GameObject LoadingCharacter;
    public GameObject LoadingObject;

    private void Awake()
    {
        if(_instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }

        _instance = this;
        DontDestroyOnLoad(gameObject);

        fadeImage = GetComponentInChildren<Image>();
    }

    public void ChangeScene(string sceneName)
    {
        fadeImage.DOFade(1, fadeDuration).OnStart(() =>
        {
            fadeImage.raycastTarget = true;
        })
            .OnComplete(() =>
        {
            StartCoroutine("LoadScene", sceneName);
        });
    }

    public IEnumerator LoadScene(string sceneName)
    {
        yield return null;
    }

    public IEnumerator FadeOut()
    {
        fadeImage.raycastTarget = true;
        Color color = fadeImage.color;
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            color.a = Mathf.Lerp(0, 1, t / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }
        color.a = 1;
        fadeImage.color = color;
    }

    public IEnumerator FadeIn()
    {
        Color color = fadeImage.color;
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            color.a = Mathf.Lerp(1, 0, t / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }
        color.a = 0;
        fadeImage.color = color;
        fadeImage.raycastTarget = false;
    }
}

