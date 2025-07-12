using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class WelcomeFader : MonoBehaviour
{
    public Image welcomeImage;
    public float fadeDuration = 1f;
    private Action onFadeComplete;

    public void FadeAndDestroy(Action callback = null)
    {
        onFadeComplete = callback;
        StartCoroutine(FadeOutAndDestroy());
    }

    private IEnumerator FadeOutAndDestroy()
    {
        float elapsed = 0f;
        Color color = welcomeImage.color;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            welcomeImage.color = color;
            yield return null;
        }
        color.a = 0f;
        welcomeImage.color = color;
        if (onFadeComplete != null)
            onFadeComplete.Invoke();
        Destroy(gameObject);
    }
}