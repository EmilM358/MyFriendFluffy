using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    [Header("Player Settings")]
    public float speed = 12f;
    public float gravity = -9.81f * 2;
    public float jumpHeight = 3f;

    [Header("Ground Check")]
    public Transform groundcheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    // ----------- More Settings -----------
    private CharacterController controller;
    Vector3 velocity;
    bool isGrounded;
    bool isMoving;
    private Vector3 lastPosition = new Vector3(0f, 0f, 0f);

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // ----------- Ground Check -----------
        isGrounded = Physics.CheckSphere(groundcheck.position, groundDistance, groundMask);

        // ----------- Reset Velocity -----------
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        // ----------- Inputs -----------
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // ----------- Moving Vector -----------
        Vector3 move = transform.right * x + transform.forward * z;

        // ----------- Player Movement -----------
        controller.Move(move * speed * Time.deltaTime);

        // ----------- Jump -----------
        if (Input.GetButtonDown("Jump") && isGrounded) 
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        // ----------- Fall -----------
        velocity.y += gravity * Time.deltaTime;
        if (!isGrounded && velocity.y < -15f)
        {
            velocity.y = -15f;
        }

        // ----------- Player Jump -----------
        controller.Move(velocity * Time.deltaTime);

        // ----------- Movement Check -----------
        if (lastPosition != gameObject.transform.position && isGrounded)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }
        lastPosition = gameObject.transform.position;
    }
}
