using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Phase1PlayerController : MonoBehaviour
{
    [SerializeField] public int playerNum;
    [SerializeField] public float moveSpeed = 7.0f;
    [SerializeField] public float grabDistance = 2.5f;
    [SerializeField] public float throwForce = 10.0f;
    [SerializeField] public Transform grabPoint; // A point representing where the grabbed player should be positioned
    [HideInInspector] public float rotationSpeed = 720f;

    private Phase1PlayerController grabberController;
    private Vector3 moveDirection;
    private Rigidbody rb;
    private GameObject grabbedPlayer;
    private bool isGrabbing = false;
    private bool beingGrab = false;
    private int counter = 0;
    private int escapePressCount = 0;
    private const int escapePressRequired = 5;

    // Input strings
    private string horizontalInput;
    private string verticalInput;
    private string grabInput;
    private string escapeInput;


    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Assign input 
        horizontalInput = playerNum == 1 ? "Horizontal" : "Horizontal2";
        verticalInput = playerNum == 1 ? "Vertical" : "Vertical2";
        grabInput = playerNum == 1 ? "Grab1" : "Grab2";
        escapeInput = playerNum == 1 ? "Escape1" : "Escape2";
    }


    void Update()
    {
        // Only allow movement and grabbing if not currently grabbed by another player
        if (!beingGrab)
        {
            // Movement input
            float moveHorizontal = Input.GetAxisRaw(horizontalInput);
            float moveVertical = Input.GetAxisRaw(verticalInput);
            moveDirection = new Vector3(moveHorizontal, 0.0f, moveVertical);

            // Adjust movement to be isometric
            moveDirection = Quaternion.Euler(0, 45, 0) * moveDirection;

            // Rotate player to face movement direction
            if (moveDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }

            // Grab input
            if (Input.GetButtonDown(grabInput))
            {
                //Debug.Log("Try Grab");
                GrabPlayer();
            }
        }

        if (isGrabbing && Input.GetButtonUp(grabInput) && grabbedPlayer != null)
        {
            counter++;

            if (counter >= 2)
            {
                ThrowPlayer();
            }
        }


        // Attempt to escape grab
        if (beingGrab)
        {
            if (Input.GetButtonDown(escapeInput))
            {
                escapePressCount++;
                Debug.Log(escapePressCount + " out of " + escapePressRequired);

                if (escapePressCount >= escapePressRequired)
                {
                    Debug.Log("Try escape");
                    EscapeGrab();
                }

            }
        }
    }

    void FixedUpdate()
    {
        // Move the player if not grabbed by another player
        if (moveDirection.magnitude > 0.1f && !beingGrab)
        {
            rb.MovePosition(rb.position + moveDirection.normalized * moveSpeed * Time.fixedDeltaTime);
        }
    }

    void GrabPlayer()
    {
        // Implement logic to grab the nearest player

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, grabDistance);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject != gameObject && hitCollider.gameObject.CompareTag("Player"))
            {
                Phase1PlayerController otherPlayerController = hitCollider.gameObject.GetComponent<Phase1PlayerController>();
                if (otherPlayerController != null && !otherPlayerController.beingGrab)
                {
                    // Grab the player
                    grabbedPlayer = hitCollider.gameObject;
                    grabbedPlayer.transform.SetParent(grabPoint);
                    grabbedPlayer.transform.localPosition = Vector3.zero;

                    // Set this player as grabbing and the other player as being grabbed
                    isGrabbing = true;
                    otherPlayerController.beingGrab = true;

                    // Store the grabber controller in the grabbed player
                    otherPlayerController.grabberController = this;

                    break;
                }
            }
        }
    }

    public void ThrowPlayer()
    {
        if (grabbedPlayer != null)
        {
            // Reset the grabbed player's transform before detaching
            grabbedPlayer.transform.localPosition = Vector3.zero;
            grabbedPlayer.transform.localRotation = Quaternion.identity;

            grabbedPlayer.transform.SetParent(null);

            // Reactivate the Rigidbody and ensure it's not kinematic
            Rigidbody grabbedRigidbody = grabbedPlayer.GetComponent<Rigidbody>();

            if (grabbedRigidbody != null)
            {
                grabbedRigidbody.isKinematic = false;

                // Throw in the direction the grabbing player is facing
                Vector3 throwDirection = transform.forward + Vector3.up;
                grabbedRigidbody.AddForce(throwDirection.normalized * throwForce, ForceMode.Impulse);
            }

            isGrabbing = false;
            beingGrab = false;
            grabbedPlayer = null;
            counter = 0;

        }
    }

    public void EscapeGrab()
    {
        // Ensure this player is being grabbed and there is a grabber
        if (beingGrab && grabberController != null)
        {
            // Release from the grabber
            transform.SetParent(null);
            beingGrab = false;
            escapePressCount = 0;

            // Reset the grabber's state
            if (grabberController.grabbedPlayer == gameObject)
            {
                grabberController.grabbedPlayer = null;
                grabberController.isGrabbing = false;
            }

            // Reset this player's grabber reference
            grabberController = null;
        }
    }
}
