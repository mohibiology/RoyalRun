using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Vector2 movement;
    Rigidbody rigidBody;
    [SerializeField] float controlSpeed = 10f;
    private void Awake() 
    {
        rigidBody = GetComponent<Rigidbody>();
    }
    
    void FixedUpdate()
    {
        ProcessTranslation();
    }

    private void ProcessTranslation()
    {
        Vector3 currentPosition = rigidBody.position;
        Vector3 moveDirection = new Vector3(movement.x,0,movement.y);
        Vector3 newPosition = currentPosition + moveDirection * (controlSpeed * Time.fixedDeltaTime);
        rigidBody.MovePosition(newPosition);
    }
    public void Move(InputAction.CallbackContext context)
    {
        movement=context.ReadValue<Vector2>();
    }
}
