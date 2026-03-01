using System.Collections;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 600;
    public TMP_Text timeText;
    public GameObject gameOverPanel;
    public GameObject player; 
    private CharacterController playerController;

    void Start()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    void Update()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime / 2;
        }
        else
        {
            timeRemaining = 0;
            if (gameOverPanel != null && !gameOverPanel.activeSelf)
            {
                gameOverPanel.SetActive(true);
                DisablePlayerControls();
            }
        }

        DisplayTime(timeRemaining);
    }

    void DisplayTime(float timeToDisplay)
    {
        if (timeToDisplay < 0)
        {
            timeToDisplay = 0;
            DisablePlayerControls();
        }
        else if (timeToDisplay > 0)
        {
            timeToDisplay += 1;
        }

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void DisablePlayerControls()
    {
        player.GetComponent<Animator>().enabled = false;
        player.GetComponent<CharacterController>().enabled = false;
        player.gameObject.SetActive(false);
    }
}
