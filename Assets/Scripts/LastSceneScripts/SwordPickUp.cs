using TMPro;
using UnityEngine;

public class SwordPickUp : MonoBehaviour
{
    public Transform playerHand; 
    public GameObject player;
    public GameObject interactionPanel; 
    public GameObject axe;

    private bool isPlayerInRange = false;
    private bool isSwordPickedUp = false;
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
        if (other.CompareTag("Player") && !isSwordPickedUp)
        {
            isPlayerInRange = true;
            if (interactionPanel != null)
            {
                interactionPanel.SetActive(true);
                TMP_Text interactionText = interactionPanel.GetComponentInChildren<TMP_Text>();
                if (interactionText != null)
                {
                    interactionText.text = "Press F to grab the sword";
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
        isSwordPickedUp = true;
        transform.SetParent(playerHand); 
        transform.localPosition = new Vector3(0.01628378f,-0.033706f,0.0544517f); 
        transform.localRotation = Quaternion.Euler(-51.194f,-38.067f,49.786f); 
        transform.localScale = new Vector3(0.2f, 0.2f, 0.2f); 
        axe.SetActive(false);
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
