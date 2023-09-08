using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JTCameraController : MonoBehaviour
{
    public Transform target; // Reference to the player's transform
    public float smoothTime = 0.3f; // Smoothing time for camera movement
    private Vector3 velocity = Vector3.zero; // Current velocity of camera movement

    void LateUpdate()
    {
        if (target != null)
        {
            // Calculate the target position for the camera
            Vector3 targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

            // Smoothly move the camera towards the target position
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        }
    }
}