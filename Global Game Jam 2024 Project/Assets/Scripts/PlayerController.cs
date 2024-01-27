using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce;
    public float movementSpeed;
    public bool isPlayer1 = true;

    private Rigidbody rb;

    private string horizontalInputAxis;
    private string jumpInputAxis;

    private int jumpCount = 0;
    private bool canJump = true;
    private bool isGrounded = false;

    private float jumpCooldown = 0.3f;
    private float lastJumpTime;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        if (isPlayer1)
        {
            horizontalInputAxis = "Horizontal_P1";
            jumpInputAxis = "Jump_P1";
        }
        else
        {
            horizontalInputAxis = "Horizontal_P2";
            jumpInputAxis = "Jump_P2";
        }
    }

    private void Update()
    {
        HandleJumpInput();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleJumpInput()
    {
        if (Time.time - lastJumpTime > jumpCooldown && Input.GetButton(jumpInputAxis) && (canJump || jumpCount < 2))
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            jumpCount++;
            lastJumpTime = Time.time;

            if (jumpCount >= 2)
            {
                canJump = false;
            }
        }
    }

    private void HandleMovement()
    {
        float horizontalInput = Input.GetAxis(horizontalInputAxis);
        rb.velocity = new Vector3(horizontalInput * movementSpeed, rb.velocity.y, rb.velocity.z);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            canJump = true;
            jumpCount = 0;
        }
    }

    public void ResetMovement()
    {
        rb.velocity = Vector3.zero;
        isGrounded = false;
        jumpCount = 0;
        canJump = true;
    }
}
