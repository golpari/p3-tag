using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedChildRotation : MonoBehaviour
{
    private Camera mainCamera;

    void Start()
    {
        // Find the main camera in the scene 
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (mainCamera != null)
        {
            // Invert the camera's rotation and apply it to the sprite
            Vector3 cameraRotation = mainCamera.transform.rotation.eulerAngles;
            transform.rotation = Quaternion.Euler(cameraRotation.x, cameraRotation.y, 0);
        }
    }
}
