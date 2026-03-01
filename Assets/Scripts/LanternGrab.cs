using TMPro;
using UnityEngine;

public class LanternGrab : MonoBehaviour
{
    public Transform playerHand; 
    public GameObject player;
    public GameObject interactionPanel; 

    private bool isPlayerInRange = false;
    private bool isLanternPickedUp = false;
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
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.F))
        {
            PickupLantern();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isLanternPickedUp)
        {
            isPlayerInRange = true;
            if (interactionPanel != null)
            {
                interactionPanel.SetActive(true);
                TMP_Text interactionText = interactionPanel.GetComponentInChildren<TMP_Text>();
                if (interactionText != null)
                {
                    interactionText.text = "Press F to grab the latern";
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

    private void PickupLantern()
    {
        player.GetComponent<Animator>().Play("PickUp");
        isLanternPickedUp = true;
        transform.SetParent(playerHand); 
        transform.localPosition = new Vector3(0.244f,-0.394f,-0.091f); 
        transform.localRotation = Quaternion.Euler(11.809f,-2.475f,32.198f); 

        if (interactionPanel != null)
        {
            interactionPanel.SetActive(false);
        }

        Collider lanternCollider = GetComponent<Collider>();
        if (lanternCollider != null)
        {
            lanternCollider.enabled = false;
        }
    }
}
