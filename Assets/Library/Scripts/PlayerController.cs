using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public InputAction MoveAction;
    public InputAction JumpAction;
    public Rigidbody playerRb;

    public float speed = 10.0f;
    public float jumpForce = 5.0f;
    private bool isGrounded = true;

    private float BottomBoundary = -2.5f;

    private Vector2 moveInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        MoveAction.Enable();
        JumpAction.Enable();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput = MoveAction.ReadValue<Vector2>();
        float moveHorizontal = moveInput.x;
        float moveVertical = moveInput.y;

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        playerRb.AddForce(movement * speed * Time.deltaTime);

        if(JumpAction.WasPressedThisFrame() && isGrounded)
        {
            playerRb.AddForce(Vector3.up * jumpForce * Time.deltaTime, ForceMode.Impulse);
            isGrounded = false;
        }

        if (transform.position.y < BottomBoundary)
        {
            transform.position = new Vector3(0f,0.5f,-7.1f);
            playerRb.linearVelocity = Vector3.zero;
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
    
}
