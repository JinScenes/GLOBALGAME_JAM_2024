using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float jumpForce;
    [SerializeField] private float movementSpeed;
    [SerializeField] private bool isPlayer1 = true;

    [SerializeField] private LayerMask groundLayer; // Ensure this only includes layers that should be considered 'ground'
    [SerializeField] private LayerMask platformLayer; // This should include the platforms you can jump through

    private Rigidbody rb;

    private string horizontalInputAxis;
    private string jumpInputAxis;
    private string downInputAxis;

    private bool isGrounded = false;
    private bool isDescending = false;

    private int jumpCount = 0;
    private float jumpCooldown = 0.3f;
    private float lastJumpTime;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        // Set up input axes based on whether this is player 1 or player 2
        horizontalInputAxis = isPlayer1 ? "Horizontal_P1" : "Horizontal_P2";
        jumpInputAxis = isPlayer1 ? "Jump_P1" : "Jump_P2";
        downInputAxis = isPlayer1 ? "Down_P1" : "Down_P2"; // Assuming Vertical axes are set up for 'W' and 'S' or 'Up' and 'Down' arrow keys

        // Set the initial layer to Player
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
        // Perform jump if cooldown is over, the jump button is pressed, and we're either grounded or haven't jumped twice yet
        if (Time.time - lastJumpTime > jumpCooldown && Input.GetButtonDown(jumpInputAxis) && (isGrounded || jumpCount < 2))
        {
            rb.velocity = new Vector3(rb.velocity.x, jumpForce, rb.velocity.z);
            isGrounded = false;
            jumpCount++;
            lastJumpTime = Time.time;

            // If we're jumping and we're not grounded, let's ignore collision with platform layer
            if (!isGrounded)
            {
                Physics.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Platform"), true);
            }
        }

        // Re-enable collision with the platform layer if we're descending
        if (rb.velocity.y <= 0)
        {
            Physics.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Platform"), false);
        }
    }

    private void HandlePassThroughPlatform()
    {
        // Check if we are pressing 'down' and we are not grounded
        if (Input.GetAxis(downInputAxis) < 0 && !isGrounded)
        {
            isDescending = true;
            // Ignore collision with the platform layer
            Physics.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Platform"), true);
        }
        else if (isDescending)
        {
            isDescending = false;
            // Stop ignoring collision with the platform layer
            Physics.IgnoreLayerCollision(gameObject.layer, LayerMask.NameToLayer("Platform"), false);
        }
    }

    private void HandleMovement()
    {
        float horizontalInput = Input.GetAxis(horizontalInputAxis);
        rb.velocity = new Vector3(horizontalInput * movementSpeed, rb.velocity.y, 0);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (groundLayer == (groundLayer | (1 << other.gameObject.layer)) && other.contacts[0].normal.y > 0.5f)
        {
            isGrounded = true;
            jumpCount = 0;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (groundLayer == (groundLayer | (1 << other.gameObject.layer)))
        {
            isGrounded = false;
        }
    }
}
