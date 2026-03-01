using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target; 
    public Vector3 offset = new Vector3(1.5f, 0.5f, 10);
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
