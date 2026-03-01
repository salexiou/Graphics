using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class EndingGame : MonoBehaviour
{
    public Image panel;
    public TextMeshProUGUI text1;
    public TextMeshProUGUI text2;
    public TextMeshProUGUI text3;
    public float fadeDuration = 2f;
    public float messageDisplayDuration = 3f;
    public float countdownInterval = 1f;

    public GameObject inv;

    private Color panelColor;

    private void Start()
    {
        panelColor = panel.color;
        text1.gameObject.SetActive(false);
        text2.gameObject.SetActive(false);
        text3.gameObject.SetActive(false);
    }

    public IEnumerator FadeToBlack()
    {
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
            panel.color = new Color(panelColor.r, panelColor.g, panelColor.b, alpha);
            yield return null;
        }
        inv.SetActive(false);
        DisplayMessage();
    }

    private void DisplayMessage()
    {
        text1.gameObject.SetActive(true);
        text2.gameObject.SetActive(true);

        StartCoroutine(StartExitCountdown());
    }

    private IEnumerator StartExitCountdown()
    {
        yield return new WaitForSeconds(messageDisplayDuration);
        text3.gameObject.SetActive(true);

        for (int i = 5; i > 0; i--)
        {
            text3.text = $"Exiting game in {i}...";
            yield return new WaitForSeconds(countdownInterval);
        }

        ExitGame();
    }

    private void ExitGame()
    {
        Application.Quit();
    }
}
