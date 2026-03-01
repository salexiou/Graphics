using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class BlackLeverManager : MonoBehaviour
{
    public GameObject interactionPanel; 
    private bool isPlayerInRange = false;
    public AudioSource audio1;
    
    public GameObject Knob;
    public bool isUp = false;
    private Quaternion initialrot;

    void Start()
    {
        if (interactionPanel != null)
        {
            interactionPanel.SetActive(false); 
        }

        initialrot = Knob.transform.localRotation;
   
    }

    void Update()
    {
        if (isPlayerInRange)
        {
            if (interactionPanel != null)
            {
                ShowUI();
                TMP_Text interactionText = interactionPanel.GetComponentInChildren<TMP_Text>();
                if (interactionText != null)
                {
                    interactionText.text = isUp ? "Press F to push the lever down" : "Press F to push the lever up";
                }
            }

          if (Input.GetKeyDown(KeyCode.F))
        {
            if (isUp)
            {
                Debug.Log("Rotating lever to initial position.");
                Knob.transform.localRotation = initialrot;
                audio1.Play();
                isUp = false;
            }
            else
            {
                Debug.Log("Rotating lever to final position.");
                Quaternion finalRotation = Quaternion.Euler(-90, 95.125f, -4.97998f);
                Knob.transform.localRotation = finalRotation;
                audio1.Play();
                isUp = true;
            }

            StartCoroutine(HideUIAfterDelay(1.0f));
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

    IEnumerator HideUIAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        HideUI();
    }

    void ShowUI()
    {
        interactionPanel.SetActive(true);
    }

    void HideUI()
    {
        interactionPanel.SetActive(false);
    }
}
