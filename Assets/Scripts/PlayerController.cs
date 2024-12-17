using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Vector2 movement;
    Rigidbody rigidBody;
    [SerializeField] float controlSpeed = 10f;
    [SerializeField] float xClamp = 3f;
    [SerializeField] float zClamp = 2f;
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
        newPosition.x = Mathf.Clamp(newPosition.x,-xClamp,xClamp);
        newPosition.z = Mathf.Clamp(newPosition.z,-zClamp,zClamp);
        rigidBody.MovePosition(newPosition);
    }
    public void Move(InputAction.CallbackContext context)
    {
        movement=context.ReadValue<Vector2>();
    }
}
