using UnityEngine;

public class CameraScene1 : MonoBehaviour
{
    public Transform target; 
    public Vector3 offset = new Vector3(0, 3f, -5f); 
    public float smoothSpeed = 0.125f; 

    private void Start()
    {
        if (offset == Vector3.zero)
        {
            offset = transform.position - target.position;
        }
    }

    private void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = smoothedPosition;

        transform.LookAt(target);
    }
}
