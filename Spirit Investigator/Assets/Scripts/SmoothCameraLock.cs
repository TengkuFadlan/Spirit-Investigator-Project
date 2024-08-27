using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCameraLock : MonoBehaviour
{
    public float cameraLerp = 1f;

    Camera mainCamera;
    Vector3 newCameraPosition;

    void Awake()
    {
        mainCamera = Camera.main;
        newCameraPosition = mainCamera.transform.position;
    }
    
    void FixedUpdate()
    {
        newCameraPosition.x = transform.position.x;
        newCameraPosition.y = transform.position.y;

        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, newCameraPosition, cameraLerp * Time.fixedDeltaTime);
    }
}