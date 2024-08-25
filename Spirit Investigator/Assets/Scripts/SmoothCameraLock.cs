using UnityEngine;

public class SmoothCameraLock : MonoBehaviour
{
    public Transform playerTransform;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    private Transform cameraTransform;

    void Awake()
    {
        cameraTransform = Camera.main.transform;

        if (playerTransform == null)
        {
            playerTransform = transform;
        }

        offset = cameraTransform.position - playerTransform.position;
    }

    void LateUpdate()
    {
        Vector3 desiredPosition = playerTransform.position + offset;

        Vector3 smoothedPosition = Vector3.Lerp(cameraTransform.position, desiredPosition, smoothSpeed);

        cameraTransform.position = smoothedPosition;
    }
}
