using System.Collections;
using UnityEngine;

public class PlayerComba : MonoBehaviour
{
    public float pushForce;
    [SerializeField] private float pushCooldown = 2.0f;
    [SerializeField] private float reflectionDuration = 0.4f;
    [SerializeField] private float reflectCooldown = 2.0f;
    public float pushDamage = 10;

    public LayerMask playerLayer;
    private Rigidbody rb;

    public float pushRange = 2.0f;

    private bool isPushing = false;
    private bool isReflecting = false;
    private bool isPushImmune = false;

    private bool canPush = true;
    private bool canReflect = true;

    public KeyCode pushKey;
    public KeyCode reflectKey;

    [SerializeField] private Material normalMaterial;
    [SerializeField] private Material reflectingMaterial;

    private PlayerController playerController;
    private Renderer playerRenderer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        playerController = GetComponent<PlayerController>();
        playerRenderer = GetComponent<Renderer>();

        if (playerController == null)
        {
            Debug.LogError("PlayerController component not found on " + gameObject.name);
        }
        if (playerRenderer == null)
        {
            Debug.LogError("Renderer component not found on " + gameObject.name);
        }
    }


    private void Update()
    {
        //HandleCombatInput();
        
        if (Input.GetKeyDown(reflectKey) && canReflect)
        {
            StartCoroutine(HandleReflection());
        }
    }

    private void FixedUpdate()
    {
        HandleCombatInput();
    }

    private void HandleCombatInput()
    {
        if (Input.GetKeyDown(pushKey) && canPush)
        {
            print("PUSHHHHHH");
            isPushing = true;
            CheckForOpponent();
            StartCoroutine(PushCooldown());
        }
        else if (Input.GetKeyUp(pushKey))
        {
            isPushing = false;
        }
    }

    private IEnumerator HandleReflection()
    {
        isReflecting = true;
        canReflect = false;
        playerRenderer.material = reflectingMaterial;
        playerController.EnableInput(false);

        yield return new WaitForSeconds(reflectionDuration);

        isReflecting = false;
        playerRenderer.material = normalMaterial;
        playerController.EnableInput(true);

        yield return new WaitForSeconds(reflectCooldown - reflectionDuration);

        canReflect = true;
    }

    private IEnumerator PushCooldown()
    {
        canPush = false;
        yield return new WaitForSeconds(pushCooldown);
        canPush = true;
    }

    private void CheckForOpponent()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, pushRange, playerLayer))
        {
            PlayerComba otherPlayer = hit.collider.GetComponent<PlayerComba>();
            if (otherPlayer != null)
            {
                ResolveCombatInteraction(otherPlayer);
            }
        }
    }

    public IEnumerator ApplyPushForce(PlayerComba otherPlayer, Vector3 pushDirection, bool isReflected)

    {
        otherPlayer.GetComponent<PlayerController>().EnableInput(false);
        otherPlayer.GetComponent<PlayerController>().GetPushed(pushDirection * pushForce);

        if (!isReflected)
        {
            otherPlayer.GetComponent<PlayerController>().TakeDamage(pushDamage);
        }

        Vector3 pushVector = pushDirection * pushForce;

        otherPlayer.rb.velocity = pushVector;

        yield return new WaitForSeconds(.2f);

        otherPlayer.rb.velocity = new Vector3(0, otherPlayer.rb.velocity.y, 0);
        otherPlayer.GetComponent<PlayerController>().EnableInput(true);
    }


    public void SetPushImmunity(bool state)
    {
        isPushImmune = state;
    }

    private void ResolveCombatInteraction(PlayerComba otherPlayer)
    {
        Vector3 pushDirection = (otherPlayer.transform.position - transform.position).normalized;
        pushDirection.y = 0;

        if (isPushing)
        {
            if (otherPlayer.isReflecting)
            {
                StartCoroutine(ApplyPushForce(this, -pushDirection, true));
                GetComponent<PlayerController>().TakeDamage(pushDamage);
            }
            else
            {
                StartCoroutine(ApplyPushForce(otherPlayer, pushDirection, false));
            }
        }

        isPushing = false;
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * pushRange);
    }
}
