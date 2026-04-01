using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    private InputSystem_Actions playerInput; // Ensure you generated this class!
    private Vector3 playerVelocity;
    private bool isGrounded;

    [Header("Movement Settings")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private float gravity = -19.62f; // Doubled for a "snappier" feel

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundRadius = 0.4f;
    [SerializeField] private LayerMask groundLayer;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        playerInput = new InputSystem_Actions();
    }

    private void OnEnable() => playerInput.Player.Enable();
    private void OnDisable() => playerInput.Player.Disable();

    void Update()
    {
        // 1. Physical Ground Check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundRadius, groundLayer);

        if (isGrounded && playerVelocity.y < 0)
        {
            playerVelocity.y = -2f; // Snaps player to ground/slopes
        }

        // 2. Horizontal Movement (relative to capsule facing)
        Vector2 input = playerInput.Player.Move.ReadValue<Vector2>();
        Vector3 move = transform.right * input.x + transform.forward * input.y;
        controller.Move(move * speed * Time.deltaTime);

        // 3. Jump (Physics formula: sqrt(height * -2 * gravity))
        if (playerInput.Player.Jump.triggered && isGrounded)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // 4. Manual Gravity Application
        playerVelocity.y += gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    // Visualizes the ground check sphere in the Scene view
    private void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
        }
    }
}