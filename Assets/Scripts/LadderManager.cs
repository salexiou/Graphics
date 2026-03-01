using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LadderManager : MonoBehaviour
{
    public GameObject interactionPanel; 

    public GameObject hatch;

    private bool isPlayerInRange = false;

    void Start()
    {
        if (interactionPanel != null)
        {
            interactionPanel.SetActive(false); 
        }
    }

    void Update()
    {
        if (isPlayerInRange && !hatch.activeSelf )
        {
            if (interactionPanel != null)
            {
                interactionPanel.SetActive(true);
                TMP_Text interactionText = interactionPanel.GetComponentInChildren<TMP_Text>();
                if (interactionText != null)
                {
                    interactionText.text = "Press F to exit from the mine";
                }
            }
        }if(isPlayerInRange && !hatch.activeSelf && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Entered");
            interactionPanel.SetActive(false); 
            SceneManager.LoadScene("ForestScene");
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
}
