using System.Collections;
using TMPro;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    public Item item;
    public GameObject interactionPanel;
    public float messageDisplayDuration = 0.1f; 
    private bool isPlayerInRange = false;
    private bool isPickedUp = false;

    void Start()
    {
        if (interactionPanel != null)
        {
            interactionPanel.SetActive(false); 
        }
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F) && !isPickedUp)
        {
            if (interactionPanel != null)
            {
                TMP_Text interactionText = interactionPanel.GetComponentInChildren<TMP_Text>();
                if (interactionText != null)
                {
                    isPickedUp = true;
                    interactionText.text = "Item stored in inventory";
                    InventoryManager.Instance.Add(item);
                    StartCoroutine(HidePanelAfterDelay(messageDisplayDuration));
                }
            }
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
                    interactionText.text = "Press F to pick up the key";
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

    private IEnumerator HidePanelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (interactionPanel != null)
        {
            interactionPanel.SetActive(false);
        }
        gameObject.SetActive(false);

    }
}
