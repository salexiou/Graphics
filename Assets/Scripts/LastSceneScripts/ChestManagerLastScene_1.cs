using System.Collections;
using TMPro;
using UnityEngine;

public class ChestManagerLastScene_1 : MonoBehaviour
{
    public GameObject interactionPanel;     public GameObject chestTop;
    public GameObject key;
    public GameObject locker;
    private bool isPlayerInRange = false;
    private bool chestUnlocked = false;
    private bool chestOpened = false;
    private bool lootCollected = false;
    public AudioSource chestOpens;
    public AudioSource chestUnlock;
    public Item itemKey1;
    public Item doorKey;
    public Item itemPot1;
    public Item itemPot2;

    public GameObject item1;
    public GameObject item2;
    public GameObject item3;

    public float messageDisplayDuration = 0.5f; 

    void Start()
    {
        if (interactionPanel != null )
        {
            interactionPanel.SetActive(false); 
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
                InventoryManager.Instance.Remove(itemKey1);
                StartCoroutine(HidePanelAfterDelay(messageDisplayDuration));
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
                interactionText.text = "Items stored in inventory";
                InventoryManager.Instance.Add(itemPot1);
                InventoryManager.Instance.Add(itemPot2);
                InventoryManager.Instance.Add(doorKey);
                item1.SetActive(false);
                item2.SetActive(false);
                item3.SetActive(false);

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
            interactionPanel.SetActive(false);
        }
    }
}
