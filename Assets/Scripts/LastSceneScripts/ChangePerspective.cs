using Cinemachine;
using UnityEngine;

public class ChangePerspective : MonoBehaviour
{
    public CinemachineVirtualCamera cam_3p; 
    public GameObject cam_stairs; 

    public GameObject stairs;

    void Start()
    {
        if (cam_stairs != null)
        {
            cam_stairs.SetActive(false);
        }
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && stairs.activeSelf)
        {
            cam_stairs.SetActive(true);
            cam_3p.Priority = 0;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") )
        {
            cam_stairs.SetActive(false);
            cam_3p.Priority = 10;
        }
    }
}
