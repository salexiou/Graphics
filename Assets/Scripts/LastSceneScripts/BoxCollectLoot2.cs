using System.Collections;
using TMPro;
using UnityEngine;

public class BoxLootCollect2 : MonoBehaviour
{
    public GameObject interactionPanel; // Reference to the panel GameObject
    private bool isPlayerInRange = false;
    private bool lootCollected = false;
    //public AudioSource chestOpens;
   // public AudioSource chestUnlock;

    public GameObject item1;

    public Item HealthPotion;

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
            if (!lootCollected)
            {
             CollectLoot();
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
                InventoryManager.Instance.Add(HealthPotion);
                item1.SetActive(false);
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
