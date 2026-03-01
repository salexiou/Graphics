using TMPro;
using UnityEngine;

public class BoxManager : MonoBehaviour
{
    public GameObject interactionPanel; 

    public GameObject cup;

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
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F))
        {
            cup.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            if (interactionPanel != null)
            {
                interactionPanel.SetActive(true);
                TMP_Text interactionText = interactionPanel.GetComponentInChildren<TMP_Text>();
                if (interactionText != null)
                {
                    interactionText.text = "Press F to open the cup";
                }
            }
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
