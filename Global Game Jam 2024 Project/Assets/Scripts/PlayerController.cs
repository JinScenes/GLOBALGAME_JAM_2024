using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    [SerializeField] private float movementSpeed;
    [SerializeField] public bool isPlayer1 = true;

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask passThroughLayer;

    private Rigidbody rb;

    private string horizontalInputAxis;
    private string jumpInputAxis;
    private string downInputAxis;

    private int jumpCount = 0;
    private bool isGrounded = false;
    private bool canJump = true;

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
            downInputAxis = "Down_P1";
        }
        else
        {
            horizontalInputAxis = "Horizontal_P2";
            jumpInputAxis = "Jump_P2";
            downInputAxis = "Down_P2";
        }
        gameObject.layer = LayerMask.NameToLayer("Player");
    }

    private void Update()
    {
        HandleJumpInput();
        HandlePassThroughPlatform();
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

    private void HandlePassThroughPlatform()
    {
        if (Input.GetAxis(downInputAxis) < 0)
        {
            gameObject.layer = LayerMask.NameToLayer("PassThrough");
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("Player");
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
            canJump = true;
            isGrounded = true;
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
