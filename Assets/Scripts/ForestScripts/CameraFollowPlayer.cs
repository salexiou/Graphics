using UnityEngine;
using Cinemachine;

public class SmoothCameraFollow : MonoBehaviour
{
    public Transform player;  
    public Vector3 offset = new Vector3(0, 2, -5);    
    public float smoothSpeed = 0.125f; 

    private CinemachineVirtualCamera vcam;
    private CinemachineFramingTransposer transposer;

    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        transposer = vcam.GetCinemachineComponent<CinemachineFramingTransposer>();
    }

    void LateUpdate()
    {
        // Calculate the desired position of the camera based on the player's position and rotation
        Vector3 desiredPosition = player.position + player.TransformDirection(offset);

        // Smoothly move the camera to the desired position
        Vector3 smoothedPosition = Vector3.Lerp(vcam.transform.position, desiredPosition, smoothSpeed);
        vcam.transform.position = smoothedPosition;

        vcam.LookAt = player;
    }
}