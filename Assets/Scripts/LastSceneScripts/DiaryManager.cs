using System.Collections;
using TMPro;
using UnityEngine;

public class DiaryManager : MonoBehaviour
{
    public GameObject interactionPanel; 
    public GameObject LaverImage;
    private bool isPlayerInRange = false;
    private bool NoteStored  = false;
    public Item Note;

    public float messageDisplayDuration = 0.5f; 

    void Start()
    {
        if (interactionPanel != null && LaverImage != null)
        {
            interactionPanel.SetActive(false); 
            LaverImage.SetActive(false);
        }
    }

    void Update()
    {
        if (isPlayerInRange)
        {
            if (!NoteStored)
            {
                StoreNote();
            }
        }
    }


    private void StoreNote()
    {
        if (interactionPanel != null)
        {
            interactionPanel.SetActive(true);
            TMP_Text interactionText = interactionPanel.GetComponentInChildren<TMP_Text>();
            if (interactionText != null)
            {
                interactionText.text = "Press F to read the diary";
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                LaverImage.SetActive(true);
                interactionText.text = "Item stored in inventory";
                InventoryManager.Instance.Add(Note);
                NoteStored = true;
                StartCoroutine(HidePanelAfterDelay(messageDisplayDuration*3));
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

    private IEnumerator HidePanelAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (interactionPanel != null)
        {
            LaverImage.SetActive(false);
            interactionPanel.SetActive(false);
        }
    }
}
