using System.Collections;
using TMPro;
using UnityEngine;

public class ChestManager : MonoBehaviour
{
    public GameObject interactionPanel; // Reference to the panel GameObject
    public GameObject codeImage;
    public GameObject chestTop;
    public GameObject key;
    public GameObject clipboard;
    public GameObject locker;
    private bool isPlayerInRange = false;
    private bool chestUnlocked = false;
    private bool chestOpened = false;
    private bool lootCollected = false;
    public AudioSource chestOpens;
    public AudioSource chestUnlock;
    public AudioSource collectPaper;
    public Item itemKey;
    public Item itemPaper;
    public Item itemTimer;

    public float messageDisplayDuration = 0.5f; 

    void Start()
    {
        if (interactionPanel != null && codeImage != null)
        {
            interactionPanel.SetActive(false); 
            codeImage.SetActive(false);
        }
    }

    void Update()
    {
        if (isPlayerInRange)
        {
            if (!chestUnlocked)
            {
                UnlockChest();
            }
            else if (!chestOpened)
            {
                OpenChest();
            }
            else if (!lootCollected)
            {
                CollectLoot();
            }
        }
    }

    private void UnlockChest()
    {
        if (!key.activeSelf && interactionPanel != null)
        {
            interactionPanel.SetActive(true);
            TMP_Text interactionText = interactionPanel.GetComponentInChildren<TMP_Text>();
            if (interactionText != null)
            {
                interactionText.text = "Press F to unlock the chest";
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                chestUnlock.Play();
                locker.SetActive(false);
                chestUnlocked = true;
                StartCoroutine(HidePanelAfterDelay(messageDisplayDuration));
                InventoryManager.Instance.Remove(itemKey);
            }
        }
    }

    private void OpenChest()
    {
        if (interactionPanel != null)
        {
            interactionPanel.SetActive(true);
            TMP_Text interactionText = interactionPanel.GetComponentInChildren<TMP_Text>();
            if (interactionText != null)
            {
                interactionText.text = "Press F to open the chest";
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                chestOpens.Play();
                chestOpened = true;
                chestTop.SetActive(false);
                StartCoroutine(HidePanelAfterDelay(messageDisplayDuration));
            }
        }
    }

    private void CollectLoot()
    {
        if (interactionPanel != null)
        {
            interactionPanel.SetActive(true);
            TMP_Text interactionText = interactionPanel.GetComponentInChildren<TMP_Text>();
            if (interactionText != null)
            {
                interactionText.text = "Press F to collect the loot";
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                collectPaper.Play();
                codeImage.SetActive(true);
                interactionText.text = "Item stored in inventory";
                InventoryManager.Instance.Add(itemPaper);
                InventoryManager.Instance.Add(itemTimer);
                clipboard.SetActive(false);
                lootCollected = true;
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
            codeImage.SetActive(false);
            interactionPanel.SetActive(false);
        }
    }
}
