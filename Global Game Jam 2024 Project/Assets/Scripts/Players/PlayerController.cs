using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    [SerializeField] public int playerNum;
    [SerializeField] public float moveSpeed = 7.0f;

    private float rotationSpeed = 720f;
    private Rigidbody rb;
    private Vector3 moveDirection;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        float moveHorizontal = Input.GetAxisRaw(playerNum == 1 ? "Horizontal" : "Horizontal2");
        float moveVertical = Input.GetAxisRaw(playerNum == 1 ? "Vertical" : "Vertical2");

        moveDirection = new Vector3(moveHorizontal, 0.0f, moveVertical);

        moveDirection = Quaternion.Euler(0, 45, 0) * moveDirection;

        if (moveDirection != Vector3.zero)
        {

            Quaternion tragetRatation = Quaternion.LookRotation(moveDirection, Vector3.up);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, tragetRatation, rotationSpeed * Time.deltaTime);
        }

    }

    void FixedUpdate()
    {
        if (moveDirection.magnitude > 0.1f)
        {
            rb.MovePosition(rb.position + moveDirection.normalized * moveSpeed * Time.fixedDeltaTime);
        }

    }
}
