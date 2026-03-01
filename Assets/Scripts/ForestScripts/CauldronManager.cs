using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System.Collections;
using Cinemachine;
using UnityEngine.UIElements;

public class CauldronManager : MonoBehaviour
{
    public AudioSource spellcast;
    public CinemachineVirtualCamera cam_3d;
    public CinemachineVirtualCamera bridge_vcam;
    public GameObject initBridge;
    public GameObject bridgeFinalPos;
    public GameObject Castle;
    private InventoryManager inventoryManager;
    public GameObject interactionPanel;
    private bool isPlayerInRange = false;
    private float messageDisplayDuration = 0.5f; 

    private void Start()
    {
        inventoryManager = InventoryManager.Instance;
        if (interactionPanel != null)
        {
            interactionPanel.SetActive(false); 
        }
        initBridge.SetActive(true);
        bridgeFinalPos.SetActive(false);
        Castle.SetActive(false);
    }

    private void Update()
    {        
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F))
        {
            CheckForItems();
        }
    }

    public void CheckForItems()
    {
        List<int> requiredItemIDs = new List<int> { 8, 9, 10, 11 };
        bool hasAllItems = true;

        foreach (int itemID in requiredItemIDs)
        {
            if (!HasItem(itemID))
            {
                hasAllItems = false;
                break;
            }
        }

        if (hasAllItems)
        {
            TMP_Text interactionText = interactionPanel.GetComponentInChildren<TMP_Text>();
            interactionText.text = "Spell casted successfully! Bridge is now revealed.";
            StartCoroutine(HidePanelAfterDelay(messageDisplayDuration * 3));
            initBridge.SetActive(false);
            bridgeFinalPos.SetActive(true);
            Castle.SetActive(true);
            spellcast.Play();
            foreach (int itemID in requiredItemIDs)
            {
                Item item = inventoryManager.items.Find(i => i.id == itemID);
                if (item != null)
                {
                    inventoryManager.Remove(item);
                }
            }
            inventoryManager.ListItems(); 
            if(cam_3d.Priority == 10)
            {
                bridge_vcam.Priority = 10;
                cam_3d.Priority = 0;
                StartCoroutine(ChangeCamera(messageDisplayDuration*7));
                gameObject.GetComponent<BoxCollider>().enabled = false;

            }
        }
        else
        {
            TMP_Text interactionText = interactionPanel.GetComponentInChildren<TMP_Text>();
            interactionText.text = "You do not have all the required items to cast the spell.";
            StartCoroutine(HidePanelAfterDelay(messageDisplayDuration * 3));
        }
    }

    private bool HasItem(int itemID)
    {
        foreach (Item item in inventoryManager.items)
        {
            if (item.id == itemID)
            {
                return true;
            }
        }

        return false;
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
                    interactionText.text = "Press F to cast the spell";
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
    }

    private IEnumerator ChangeCamera(float delay)
    {
        yield return new WaitForSeconds(delay);
            if(bridge_vcam.Priority == 10)
            {
                bridge_vcam.Priority = 0;
                cam_3d.Priority = 10;
            }
    }
}
