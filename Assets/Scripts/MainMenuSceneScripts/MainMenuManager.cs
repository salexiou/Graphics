using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MainMenuManager : MonoBehaviour
{
    public Button startButton;
    public Button saveButton;
    public Button exitButton;
    public Sprite buttonPressedSprite1;
    public Sprite buttonPressedSprite2;
    public GameObject interactionPanel;
    public GameObject panel2;
    private Sprite startButtonDefaultSprite;
    private Sprite saveButtonDefaultSprite;
    private Sprite exitButtonDefaultSprite;
    public AudioSource audioClipButtonPress;

    void Start()
    {
        interactionPanel.SetActive(false);
        startButtonDefaultSprite = startButton.GetComponent<Image>().sprite;
        saveButtonDefaultSprite = saveButton.GetComponent<Image>().sprite;
        exitButtonDefaultSprite = exitButton.GetComponent<Image>().sprite;
        panel2.SetActive(false);
        startButton.onClick.AddListener(() => StartCoroutine(OnStartButtonPressed()));
        startButton.onClick.AddListener(() => StartCoroutine(OnButtonPress(startButton, startButtonDefaultSprite, buttonPressedSprite1)));
        saveButton.onClick.AddListener(() => StartCoroutine(OnSaveButtonPressed()));
        saveButton.onClick.AddListener(() => StartCoroutine(OnButtonPress(saveButton, saveButtonDefaultSprite, buttonPressedSprite2)));
        exitButton.onClick.AddListener(() => StartCoroutine(OnExitButtonPressed()));
        exitButton.onClick.AddListener(() => StartCoroutine(OnButtonPress(exitButton, exitButtonDefaultSprite, buttonPressedSprite2)));
    }

    IEnumerator OnButtonPress(Button button, Sprite defaultSprite, Sprite pressedSprite)
    {
        audioClipButtonPress.Play();
        button.GetComponent<Image>().sprite = pressedSprite;
        yield return new WaitForSeconds(0.2f);
        button.GetComponent<Image>().sprite = defaultSprite;
    }

    IEnumerator OnStartButtonPressed()
    {
        yield return StartCoroutine(OnButtonPress(startButton, startButtonDefaultSprite, buttonPressedSprite1));
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene("StartScene");
    }

    IEnumerator OnSaveButtonPressed()
    {
        yield return StartCoroutine(OnButtonPress(saveButton, saveButtonDefaultSprite, buttonPressedSprite2));
        yield return new WaitForSeconds(0.2f);
        panel2.SetActive(true);
        gameObject.SetActive(false);
    }

    IEnumerator OnExitButtonPressed()
    {
        yield return StartCoroutine(OnButtonPress(exitButton, exitButtonDefaultSprite, buttonPressedSprite2));
        yield return new WaitForSeconds(0.2f);
        Application.Quit();
    }
}
