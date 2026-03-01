using System.Collections;
using TMPro;
using UnityEngine;

public class LaverController : MonoBehaviour
{
    public GameObject interactionPanel; 
    public GameObject Portrait;
    public GameObject laver;
    public GameObject stairs;
    public GameObject floor;

    private bool isPlayerInRange = false;
    private bool PortraitMoved = false;
    private bool LaverMoved = false;

    public AudioSource laverSnd;


    public float messageDisplayDuration = 0.5f; 

    void Start()
    {
        if (interactionPanel != null )
        {
            interactionPanel.SetActive(false); 
            laver.SetActive(false);
            stairs.SetActive(false);
            floor.SetActive(true);
        }
    }

    void Update()
    {
        if (isPlayerInRange)
        {
            if (!PortraitMoved)
            {
                MovePortrait();
            }
            else if (!LaverMoved)
            {
                MoveLaver();
            }
        }
    }

    private void MovePortrait()
    {
        if (interactionPanel != null)
        {
            interactionPanel.SetActive(true);
            TMP_Text interactionText = interactionPanel.GetComponentInChildren<TMP_Text>();
            if (interactionText != null)
            {
                interactionText.text = "Press F to move the portrait";
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                Portrait.transform.position = new Vector3(Portrait.transform.position.x, Portrait.transform.position.y, Portrait.transform.position.z - 1.3f);
                PortraitMoved = true;
                laver.SetActive(true);
                StartCoroutine(HidePanelAfterDelay(messageDisplayDuration));
            }
        }
    }

    private void MoveLaver()
    {
        if (interactionPanel != null)
        {
            interactionPanel.SetActive(true);
            TMP_Text interactionText = interactionPanel.GetComponentInChildren<TMP_Text>();
            if (interactionText != null)
            {
                interactionText.text = "Press F to flip the laver";
            }

            if (Input.GetKeyDown(KeyCode.F))
            {
                Vector3 finalPosition = new Vector3(-0.277f, 1.284f, 0.54f);
                Quaternion finalRotation = Quaternion.Euler(-12.421f, 95.043f, 0);
                laverSnd.Play();
                laver.transform.localPosition = finalPosition;
                laver.transform.localRotation = finalRotation;

                LaverMoved = true;
                stairs.SetActive(true);
                floor.SetActive(false);
                StartCoroutine(HidePanelAfterDelay(messageDisplayDuration));
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
