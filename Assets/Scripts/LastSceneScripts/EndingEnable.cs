using UnityEngine;
using Cinemachine;

public class EndingEnable : MonoBehaviour
{
    public Transform cameraTransform;
    public GameObject targetObject;
    public float moveSpeed = 0.3f;
    public float rotateSpeed = 3.0f;
    public float duration = 2.0f;
    public string playerTag = "Player";

    private float elapsedTime = 0.0f;
    private bool isActivated = false;
    public CinemachineVirtualCamera vcam;
    public EndingGame endingGameScript;

    private void Start()
    {
        if (targetObject != null)
        {
            targetObject.SetActive(false);
        }

        if (cameraTransform != null)
        {
            cameraTransform.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (isActivated)
        {
            if (elapsedTime < duration)
            {
                float moveStep = moveSpeed * Time.deltaTime;
                float rotateStep = rotateSpeed * Time.deltaTime;

                cameraTransform.position += new Vector3(0, moveStep, 0);

                cameraTransform.Rotate(-Vector3.left * rotateStep);

                elapsedTime += Time.deltaTime;
            }
            else
            {
                if (targetObject != null)
                {
                    targetObject.SetActive(true);
                    if (endingGameScript != null)
                    {
                        StartCoroutine(endingGameScript.FadeToBlack()); 
                    }
                }
                isActivated = false;
            }
        }
    }

    public void ActivateMovement()
    {
        isActivated = true;
        elapsedTime = 0.0f;

        if (vcam != null)
        {
            vcam.Priority = 0;
        }
        if (cameraTransform != null)
        {
            cameraTransform.gameObject.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            ActivateMovement();
        }
    }
}
