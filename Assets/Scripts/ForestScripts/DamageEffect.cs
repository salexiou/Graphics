using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageEffect : MonoBehaviour
{
    public Image damageOverlay;
    public float fadeDuration = 1f;

    private Color overlayColor;
    private Coroutine fadeCoroutine;

    void Start()
    {
        overlayColor = damageOverlay.color;
        overlayColor.a = 0;
        damageOverlay.color = overlayColor;
        damageOverlay.gameObject.SetActive(false);
    }

    public void TriggerDamageEffect()
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }

        damageOverlay.gameObject.SetActive(true);
        overlayColor.a = 0.5f; 
        damageOverlay.color = overlayColor;

        fadeCoroutine = StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            overlayColor.a = Mathf.Lerp(0.5f, 0, elapsedTime / fadeDuration);
            damageOverlay.color = overlayColor;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        overlayColor.a = 0;
        damageOverlay.color = overlayColor;
        damageOverlay.gameObject.SetActive(false);
    }
}
