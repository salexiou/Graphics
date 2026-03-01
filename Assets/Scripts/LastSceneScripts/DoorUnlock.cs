using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class DoorUnlock : MonoBehaviour
{
    public GameObject interactionPanel; 
    public GameObject key;
    public Item doorKey;

    public GameObject Evelyn;
    private bool isPlayerInRange = false;
    private bool isOpened = false;


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
        if (isPlayerInRange && !isOpened)
        {
            if (key.activeSelf)
            {
                DoorLocked();
            }
            else if (!key.activeSelf)
            {
                DoorUnlocking();
            }
        }
    }

    private void DoorLocked()
    {
        if (interactionPanel != null)
        {
            interactionPanel.SetActive(true);
            TMP_Text interactionText = interactionPanel.GetComponentInChildren<TMP_Text>();
            if (interactionText != null)
            {
                interactionText.text = "The door is locked";
            }
        }
    }

    private void DoorUnlocking()
    {
        if (interactionPanel != null)
        {
            interactionPanel.SetActive(true);
            TMP_Text interactionText = interactionPanel.GetComponentInChildren<TMP_Text>();
            if (interactionText != null)
            {
                interactionText.text = "Press F to open the door";
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                Quaternion finalRotation = Quaternion.Euler(0, 100, 0);

                transform.localRotation = finalRotation;
                InventoryManager.Instance.Remove(doorKey);


                isOpened = true;
                gameObject.GetComponent<BoxCollider>().enabled = false;
                gameObject.GetComponentInParent<MeshCollider>().enabled = false;
                gameObject.AddComponent<MeshCollider>().enabled = true;
                StartCoroutine(HidePanelAfterDelay(messageDisplayDuration));
                Evelyn.SetActive(true);
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
