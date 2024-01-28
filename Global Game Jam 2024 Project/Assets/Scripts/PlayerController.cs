using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float groundCheckDistance = .1f;
    [SerializeField] private float gravityMultiplier = 2.5f;

    [SerializeField] private LayerMask groundLayer;

    private Rigidbody rb;

    private string horizontalInputAxis;
    private string jumpInputAxis;
    private string downInputAxis;

    private int jumpCount = 0;
    
    private bool canJump = true;
    private bool allowInput = true;
    public bool isPlayer1 = true;

    private float jumpCooldown = 0.3f;
    private float lastJumpTime;
    private float controlInversionFactor = 1f;

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
        CheckGrounded();
        ApplyGravity();

        if (allowInput)
        {
            HandleMovement();
        }
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
        float horizontalInput = Input.GetAxis(horizontalInputAxis) * controlInversionFactor;
        if (horizontalInput > 0)
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        else if (horizontalInput < 0)
        {
            transform.rotation = Quaternion.Euler(0, -90, 0);
        }

        rb.velocity = new Vector3(horizontalInput * movementSpeed, rb.velocity.y, rb.velocity.z);
    }

    public void EnableInput(bool enable)
    {
        allowInput = enable;
        if (!enable)
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
    }
    
    private void CheckGrounded()
    {
        RaycastHit hit;

        bool isGrounded = Physics.Raycast(transform.position, Vector3.down, out hit, groundCheckDistance, groundLayer);
        Debug.DrawRay(transform.position, Vector3.down * groundCheckDistance, isGrounded ? Color.green : Color.red);

        if (isGrounded)
        {
            canJump = true;
            jumpCount = 0;
        }
        else
        {
            canJump = false;
        }
    }

    private void ApplyGravity()
    {
        if (!canJump && rb.velocity.y < 0)
        {
            rb.AddForce(Vector3.down * gravityMultiplier, ForceMode.Acceleration);
        }
    }

    public void ResetMovement()
    {
        rb.velocity = Vector3.zero;
        jumpCount = 0;
        canJump = true;
    }

    public void InvertControls(float intensity)
    {
        controlInversionFactor = intensity;
    }
}
