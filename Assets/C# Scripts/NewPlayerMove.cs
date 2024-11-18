using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BallController : MonoBehaviour
{
    public TextMeshProUGUI countText;
    public GameObject winTextObject;
    
    public float moveSpeed = 5f;
    public float sprintMultiplier = 2f;
    public float rotationSpeed = 100f;
    public float decelerationTime = 2f;  // Time it takes for the ball to decelerate to stop

    private Rigidbody rb;
    private Camera mainCamera;

    private Vector3 moveDirection;
    private Vector3 currentVelocity;
    private float sprintTimer = 0f;
    private int count;

    void Start()
    {
        // Get the Rigidbody component for physics-based movement
        rb = GetComponent<Rigidbody>();

        // Get the main camera
        mainCamera = Camera.main;
        count = 0;
        SetCountText();
        winTextObject.SetActive(false);
    }

    void Update()
    {
        // Handle movement input based on the camera's facing direction
        HandleMovement();

        // Handle rotation of the ball based on right-click and mouse dragging
        HandleRotation();

        // Handle sprint (Shift key for 3 seconds)
        HandleSprint();
    }

    private void HandleMovement()
    {
        // Get input for movement (WASD or Arrow Keys)
        float moveX = Input.GetAxis("Horizontal"); // A/D or Left/Right
        float moveZ = Input.GetAxis("Vertical");   // W/S or Up/Down

        // Get the camera's forward and right directions (camera-relative movement)
        Vector3 cameraForward = mainCamera.transform.forward;
        Vector3 cameraRight = mainCamera.transform.right;

        // Ensure the camera's forward direction is flat (ignore vertical tilting)
        cameraForward.y = 0f;
        cameraForward.Normalize();
        cameraRight.y = 0f;
        cameraRight.Normalize();

        // Calculate the movement direction based on the camera's facing
        moveDirection = (cameraForward * moveZ + cameraRight * moveX).normalized;

        // If Sprint is active, double the speed
        float speed = (sprintTimer > 0f) ? moveSpeed * sprintMultiplier : moveSpeed;

        // Apply movement to the Rigidbody, with a small deceleration after releasing the movement keys
        currentVelocity = Vector3.Lerp(currentVelocity, moveDirection * speed, decelerationTime * Time.deltaTime);
        rb.MovePosition(transform.position + currentVelocity * Time.deltaTime);
    }

    private void HandleRotation()
    {
        if (Input.GetMouseButton(1))  // Right mouse button held
        {
            // Rotate the ball around the Y-axis based on the horizontal mouse movement (dragging)
            float mouseDeltaX = Input.GetAxis("Mouse X");
            transform.Rotate(0f, mouseDeltaX * rotationSpeed * Time.deltaTime, 0f, Space.World);
        }
    }

    private void HandleSprint()
    {
        // When Shift is held, start the sprint timer
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            sprintTimer = Mathf.Clamp(sprintTimer + Time.deltaTime, 0f, 3f);
        }
        else
        {
            sprintTimer = Mathf.Max(0f, sprintTimer - Time.deltaTime);  // Reduce timer when Shift is not held
        }
    }

    private void SetCountText()
    {
        countText.text = "Count: " + count.ToString() + "/13";
        if(count >= 13)
        {
            winTextObject.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
    }
}