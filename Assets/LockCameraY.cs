using UnityEngine;
using Cinemachine;

public class LockCameraY : MonoBehaviour
{
    private CinemachineVirtualCamera vcam;
    private float fixedY;

    void Start()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        if (vcam != null)
        {
            fixedY = vcam.transform.position.y;
        }
    }

    void LateUpdate()
    {
        if (vcam != null)
        {
            Vector3 newPosition = vcam.transform.position;
            newPosition.y = fixedY; 
            vcam.transform.position = newPosition;
        }
    }
}
