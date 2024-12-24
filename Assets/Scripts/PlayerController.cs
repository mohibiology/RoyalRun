using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Rigidbody rigidBody;

    [SerializeField] float laneDistance = 2.5f; // Distance between lanes
    [SerializeField] float laneSwitchSpeed = 10f; // Speed to switch lanes

    private int currentLane = 1; // 0 = Left, 1 = Middle, 2 = Right
    private Vector3 targetPosition;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        // Initialize the target position to the middle lane
        targetPosition = rigidBody.position;
    }

    private void FixedUpdate()
    {
        ProcessMovement();
    }

    private void ProcessMovement()
    {
        // Smoothly move the player to the target lane position
        Vector3 currentPosition = rigidBody.position;
        Vector3 newPosition = Vector3.MoveTowards(currentPosition, targetPosition, laneSwitchSpeed * Time.fixedDeltaTime);
        rigidBody.MovePosition(newPosition);
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            float input = context.ReadValue<Vector2>().x; // Only consider horizontal input

            if (input > 0 && currentLane < 2)
            {
                // Move right
                currentLane++;
            }
            else if (input < 0 && currentLane > 0)
            {
                // Move left
                currentLane--;
            }

            // Update the target position based on the current lane
            targetPosition = new Vector3((currentLane-1) * laneDistance, rigidBody.position.y, rigidBody.position.z);
        }
    }
}
