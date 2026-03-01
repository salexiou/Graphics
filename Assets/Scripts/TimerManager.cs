using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public GameObject interactivePanel;
    public Light targetLight; 
    public float lightDuration = 0.1f;
    public float lightOffDuration = 0.1f; 
    public AudioSource sirenAudio;
    private bool isPlayerInRange = false;
    private bool isLightCoroutineRunning = false;

    void Start()
    {
        targetLight.enabled = false;
        interactivePanel.SetActive(false); 
    }

    void Update()
    {
         if (isPlayerInRange)
        {
            if (!isLightCoroutineRunning)
            {
                StartCoroutine(ToggleLight());
                
            }
        }
        else
        {
            if (isLightCoroutineRunning)
            {
                StopCoroutine(ToggleLight());
                targetLight.enabled = false;
                isLightCoroutineRunning = false;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
         if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            interactivePanel.SetActive(true); 
        }
    }
    IEnumerator ToggleLight()
    {
        isLightCoroutineRunning = true;
        sirenAudio.Play();

        while (true)
        {
            targetLight.enabled = true;
            yield return new WaitForSeconds(lightDuration);

            targetLight.enabled = false;
            yield return new WaitForSeconds(lightOffDuration);
        }
    }
}
