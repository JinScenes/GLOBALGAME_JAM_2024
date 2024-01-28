using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public int playerNum;
    [SerializeField] public float moveSpeed = 7.0f;
    [SerializeField] public float grabDistance = 2.5f;
    [SerializeField] public Transform grabPoint; // A point representing where the grabbed player should be positioned
    [HideInInspector] public float rotationSpeed = 720f;


    private Vector3 moveDirection;
    private Rigidbody rb;
    private GameObject grabbedPlayer;
    private bool isGrabbing = false;
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
        if (!isGrabbing)
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
                Debug.Log("Try Grab");
                GrabPlayer();
            }
        }
        else
        {
            // Attempt to escape grab
            if (Input.GetButtonDown(escapeInput))
            {
                escapePressCount++;
                Debug.Log(escapePressCount + " to " + escapePressRequired);
                if (escapePressCount >= escapePressRequired)
                {

                    EscapeGrab();
                }
            }
        }
    }

    void FixedUpdate()
    {
        // Move the player if not grabbed by another player
        if (moveDirection.magnitude > 0.1f && !isGrabbing)
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
                PlayerController otherPlayerController = hitCollider.gameObject.GetComponent<PlayerController>();
                if (otherPlayerController != null && !otherPlayerController.isGrabbing)
                {
                    // Grab the player
                    grabbedPlayer = hitCollider.gameObject;
                    grabbedPlayer.transform.SetParent(grabPoint);
                    grabbedPlayer.transform.localPosition = Vector3.zero;
                    grabbedPlayer.GetComponent<PlayerController>().isGrabbing = true;
                    break;
                }
            }
        }
    }

    public void EscapeGrab()
    {
        // Release the grabbed player
        if (grabbedPlayer != null)
        {
            grabbedPlayer.transform.SetParent(null);
            grabbedPlayer.GetComponent<PlayerController>().isGrabbing = false;
            grabbedPlayer = null;
            escapePressCount = 0; // Reset the escape press count
        }
    }
}