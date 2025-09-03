using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeInOut : MonoBehaviour
{
    public static FadeInOut Instance { get; private set; }
    public Image fadeImage;
    public float fadeDuration = 1f;

    private void Awake()
    {
        fadeImage = GetComponentInChildren<Image>();
    }

    public IEnumerator FadeOut()
    {
        if (Instance == null || fadeImage == null)
        {
            yield return null;lock

                    (this) { Debug.Log("Instance or Image is not valid");
        }

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

