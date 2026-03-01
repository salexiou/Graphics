using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChangeScene1 : MonoBehaviour
{
    public GameObject currentCamera;  
    public GameObject newCamera;     

    private bool isPlayerInRange = false;

    void Start()
    {
        if (newCamera != null)
        {
            newCamera.SetActive(false);  
        }
    }

    void Update()
    {
        if (isPlayerInRange)
        {
            ChangeCamera();
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
            currentCamera.SetActive(true);      
            newCamera.SetActive(true); 
        }
    }

    private void ChangeCamera()
    {
        if (currentCamera != null && newCamera != null)
        {
            currentCamera.SetActive(false);      
            newCamera.SetActive(true);      
        }
    }
}
