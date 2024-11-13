using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;  // The ball (player) to follow
    public Vector3 offset = new Vector3(0, 5, -10);  // Default camera offset (behind and above the ball)
    public float rotateSpeed = 5f;  // Speed of camera rotation around the ball
    public float smoothSpeed = 0.125f;  // Smoothness of the camera movement

    private float currentRotationAngle = 0f;
    private float currentVerticalAngle = 10f;

    void LateUpdate()
    {
        // Rotate the camera based on mouse movement
        HandleRotation();

        // Follow the ball with the calculated offset
        FollowBall();
    }

    private void HandleRotation()
    {
        if (Input.GetMouseButton(1))  // Right mouse button held
        {
            // Horizontal rotation based on mouse X-axis movement
            float horizontalInput = Input.GetAxis("Mouse X") * rotateSpeed;
            currentRotationAngle += horizontalInput;

            // Vertical rotation based on mouse Y-axis movement (not inverted)
            float verticalInput = Input.GetAxis("Mouse Y") * rotateSpeed;  // Direction is not inverted
            currentVerticalAngle = Mathf.Clamp(currentVerticalAngle + verticalInput, -40f, 80f);  // Clamped to prevent flipping
        }
    }

    private void FollowBall()
    {
        if (target != null)
        {
            // Calculate the desired camera rotation
            Quaternion rotation = Quaternion.Euler(currentVerticalAngle, currentRotationAngle, 0);

            // Calculate the direction the camera should look in
            Vector3 direction = rotation * Vector3.forward;

            // Calculate the new position for the camera (based on offset and rotation)
            Vector3 desiredPosition = target.position + direction * offset.magnitude;

            // Smoothly move the camera to the new position
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Ensure the camera is always looking at the ball
            transform.LookAt(target);
        }
    }
}