using TMPro;
using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.UIElements;
public class AxePickUp : MonoBehaviour
{
    public Transform playerHand; 
    public GameObject player;
    public GameObject interactionPanel; 
    public PlayerMovement1 playerMovement;

    private bool isPlayerInRange = false;
    private bool isAxePickedUp = false;
    private Transform originalParent;

    void Start()
    {
        if (interactionPanel != null)
        {
            interactionPanel.SetActive(false); 
        }
        originalParent = transform.parent; 
    }

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F) && !isAxePickedUp)
        {
            PickupAxe();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isAxePickedUp)
        {
            isPlayerInRange = true;
            if (interactionPanel != null)
            {
                interactionPanel.SetActive(true);
                TMP_Text interactionText = interactionPanel.GetComponentInChildren<TMP_Text>();
                if (interactionText != null)
                {
                    interactionText.text = "Press F to grab the axe";
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

    private void PickupAxe()
    {
        isAxePickedUp = true;
        playerMovement.isAxePickedUp = true; 
        
        if (interactionPanel != null)
            {
                interactionPanel.SetActive(true);
                TMP_Text interactionText = interactionPanel.GetComponentInChildren<TMP_Text>();
                if (interactionText != null)
                {
                    interactionText.text = "Press G to attack to the zombies";
                }
            }

        transform.SetParent(playerHand); 
        transform.localPosition = new Vector3(0.0041f, -0.0003f, 0.0441f); 
        transform.localRotation = Quaternion.Euler(117.437f, -274.671f, 101.479f); 
        transform.localScale = new Vector3(0.15f, 0.15f, 0.15f); 

        StartCoroutine(HidePanelAfterDelay(2.0f)); 

        Collider axeCollider = GetComponent<Collider>();
        if (axeCollider != null)
        {
            axeCollider.enabled = false;
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
