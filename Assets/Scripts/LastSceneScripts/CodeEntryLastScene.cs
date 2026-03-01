using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class CodeEntryLastScene : MonoBehaviour
{
    public GameObject interactionPanel; 

    public GameObject passwordUI;
    private bool isPlayerInRange = false;
    public TMP_Text inputField;
    public Button[] numberButtons;
    public Button enterButton;
    public Button resetButton;
    public Button deleteButton;
    public Button closeButton;

    public Sprite numberPressedSprite;
    public Sprite actionPressedSprite;

    public AudioSource audioClip;
    public AudioSource audioClipButtonPress;
    public AudioSource audioClipWrongCode;

    private Sprite defaultSprite;
    private string correctCode = "90210";
    private string enteredCode = "";

    public GameObject ShieldMetallCup;
    public GameObject ShieldMetallMainKnob;

    public GameObject Floor;
    public GameObject Stairs;

    private bool isUIActive = false;

    void Start()
    {
        Floor.SetActive(true);
        Stairs.SetActive(false);

        if (interactionPanel != null)
        {
            interactionPanel.SetActive(false); 
        }
        
        Time.timeScale = 1;

        HideUI();

        foreach (Button button in numberButtons)
        {
            button.onClick.AddListener(() => OnNumberButtonPressed(button.GetComponentInChildren<TextMeshProUGUI>().text));
            button.onClick.AddListener(() => OnButtonPress(button, numberPressedSprite));
        }

        enterButton.onClick.AddListener(OnEnterButtonPressed);
        enterButton.onClick.AddListener(() => OnButtonPress(enterButton, actionPressedSprite));
        resetButton.onClick.AddListener(OnResetButtonPressed);
        resetButton.onClick.AddListener(() => OnButtonPress(resetButton, actionPressedSprite));
        deleteButton.onClick.AddListener(OnDeleteButtonPressed);
        deleteButton.onClick.AddListener(() => OnButtonPress(deleteButton, actionPressedSprite));
        closeButton.onClick.AddListener(() => OnCloseButtonPressed(closeButton, actionPressedSprite));

        UpdateButtonStates();
    }

    void Update()
    {
        if (ShieldMetallCup.activeSelf && !isUIActive && isPlayerInRange){
            if (interactionPanel != null)
            {
                interactionPanel.SetActive(true);
                TMP_Text interactionText = interactionPanel.GetComponentInChildren<TMP_Text>();
                if (interactionText != null)
                {
                    interactionText.text = "Press F to open the cup";
                }
            }
            if (Input.GetKeyDown(KeyCode.F)){
                ShieldMetallCup.SetActive(false);
            }
        }else if(!ShieldMetallCup.activeSelf && !isUIActive && isPlayerInRange){
            if (interactionPanel != null)
            {
                interactionPanel.SetActive(true);
                TMP_Text interactionText = interactionPanel.GetComponentInChildren<TMP_Text>();
                if (interactionText != null)
                {
                    interactionText.text = "Press F to enter the code";
                }
            }
            if (Input.GetKeyDown(KeyCode.F)){
                interactionPanel.SetActive(false);
                ShowUI();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            if (interactionPanel != null)
            {
                interactionPanel.SetActive(false);
            }
        }
    }

    void OnCloseButtonPressed(Button button, Sprite pressedSprite)
    {
        defaultSprite = button.GetComponent<Image>().sprite;
        button.GetComponent<Image>().sprite = pressedSprite;
        HideUI();
        isUIActive = false;
    }

    void OnNumberButtonPressed(string number)
    {
        if (enteredCode.Length < 5)
        {
            audioClipButtonPress.Play();
            enteredCode += number;
            UpdateInputField();
            UpdateButtonStates();
        }
    }

    void OnButtonPress(Button button, Sprite pressedSprite)
    {
        audioClipButtonPress.Play();
        defaultSprite = button.GetComponent<Image>().sprite;
        button.GetComponent<Image>().sprite = pressedSprite;
        StartCoroutine(ResetButtonSprite(button));
    }

    IEnumerator ResetButtonSprite(Button button)
    {
        yield return new WaitForSeconds(0.1f);
        button.GetComponent<Image>().sprite = defaultSprite;
    }

    void OnEnterButtonPressed()
    {
        audioClipButtonPress.Play();
        if (enteredCode == correctCode)
        {
            inputField.text = "Correct password! The stairs are now revealed!";
            audioClip.Play();

            Vector3 finalPosition = new Vector3(-0.4908f, -0.0799f, 0.0575f);
            Quaternion finalRotation = Quaternion.Euler(-51.406f, 351.748f, 10.73f);
            ShieldMetallMainKnob.transform.localPosition = finalPosition;
            ShieldMetallMainKnob.transform.localRotation = finalRotation;
            Stairs.SetActive(true);
            Floor.SetActive(false);    
            StartCoroutine(HideUIAfterDelay(1f));
        }
        else
        {
            audioClipWrongCode.Play();
            inputField.text = "Wrong password! Please try again!";
            StartCoroutine(HideMessageAfterDelay(1f));
        }
    }

    IEnumerator HideUIAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        HideUI();
    }

    IEnumerator HideMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        OnResetButtonPressed();
    }

    void OnResetButtonPressed()
    {
        audioClipButtonPress.Play();
        enteredCode = "";
        UpdateInputField();
        UpdateButtonStates();
    }

    void OnDeleteButtonPressed()
    {
        audioClipButtonPress.Play();
        if (enteredCode.Length > 0)
        {
            enteredCode = enteredCode.Substring(0, enteredCode.Length - 1);
            UpdateInputField();
            UpdateButtonStates();
        }
    }

    void UpdateInputField()
    {
        inputField.text = enteredCode;
    }

    void UpdateButtonStates()
    {
        enterButton.interactable = enteredCode.Length == 5;
        resetButton.interactable = enteredCode.Length > 0;
        deleteButton.interactable = enteredCode.Length > 0;
    }

    void ShowUI()
    {
        passwordUI.SetActive(true);
        isUIActive = true;
    }

    void HideUI()
    {
        passwordUI.SetActive(false);
        isUIActive = false;
    }
}
