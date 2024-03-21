using UnityEngine.InputSystem;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float sensibility = 18f;
    public float baseSpeed = 0.1f;
    public float cameraFov = 60f;
    public float walkingVelocityCap = 4f;
    public float fallingVelocityCap = 5f;
    public float jumpForce = 65f;

    private bool isRunning = false;
    private bool isCrouching = false;
    private bool isGrounded;
    private float currentSpeed = 0f;
    private Animator animator;
    private Rigidbody rb;
    private Vector2 cameraMovement;
    private Vector2 playerMovement;
    private Camera thirdPersonCamera;
    private Camera firstPersonCamera;
    private Canvas playerReticule;
    private float yTurn = 0f;

    // Setting player stuff up
    void Awake()
    {
        // Get components
        animator = transform.GetChild(0).GetComponent<Animator>();
        thirdPersonCamera = transform.Find("Cameras").Find("Third-Person View").GetComponent<Camera>();
        firstPersonCamera = transform.Find("Cameras").Find("First-Person View").GetComponent<Camera>();
        playerReticule = transform.Find("Reticule").GetComponent<Canvas>();
        rb = GetComponent<Rigidbody>();

        // Set game stuff up
        Cursor.lockState = CursorLockMode.Locked;
        thirdPersonCamera.fieldOfView = cameraFov;
        firstPersonCamera.fieldOfView = cameraFov;
    }

    // Update is called once per frame
    void Update()
    {
        // Walking and running animations.
        if (playerMovement == Vector2.zero)
        {
            animator.SetBool("Walking", false);
            isRunning = false;
        }
        else { animator.SetBool("Walking", true); }

        // Running checks and animations.
        if (isRunning) currentSpeed = baseSpeed * 1.5f;
        else currentSpeed = baseSpeed;
        animator.SetBool("Running", isRunning);

        // Crouching checks and animations
        if (isCrouching) animator.SetLayerWeight(1, 1);
        else animator.SetLayerWeight(1, 0);

        // Sets the falling bool
        animator.SetBool("Falling", !isGrounded);
    }

    // Physics simulations on FixedUpdate
    void FixedUpdate()
    {
        MovePlayer();
    }

    // Camera movement
    void LateUpdate()
    {
        // Updates camera position
        UpdateLookingPosition();
    }

    // Move camera
    private void UpdateLookingPosition()
    {
        // Rotate Left/Right, any camera
        transform.Rotate(sensibility * cameraMovement.x * Time.deltaTime * Vector3.up);

        // Rotate Up/Down, only third person camera
        if (thirdPersonCamera.gameObject.activeSelf)
        {
            // Calculate camera Y
            yTurn += cameraMovement.y * sensibility * Time.deltaTime;

            // Limit camera Y axis
            if (yTurn > 10f) yTurn = 10f;
            else if (yTurn < -40f) yTurn = -40f;

            // Move it
            thirdPersonCamera.transform.localRotation = Quaternion.Euler(-yTurn, 0, 0);
        }
    }

    // Moves the player
    private void MovePlayer()
    {
        transform.Translate(playerMovement.y * currentSpeed * Vector3.forward);
        transform.Translate(playerMovement.x * currentSpeed * Vector3.right);
    }

    // Get the player's camera input
    private void OnLook(InputValue ctx) { cameraMovement = ctx.Get<Vector2>(); }

    // Get the player's movement input
    private void OnMove(InputValue ctx) { playerMovement = ctx.Get<Vector2>(); }

    // Toggle player run
    private void OnRun() { isRunning = !isRunning; if (isCrouching && isRunning) ToggleCrouch(false); }

    // Toggle crouch
    private void OnCrouch() { isCrouching = !isCrouching; ToggleCrouch(isCrouching); }

    // Dance
    private void OnDance()
    {
        animator.SetBool("Dance", !animator.GetBool("Dance"));
    }

    // Backflip
    private void OnBackflip()
    {
        animator.SetBool("Backflip", !animator.GetBool("Backflip"));
    }

    // Jump
    private void OnJump()
    {
        if (!isGrounded || rb.velocity.y > 0) return;

        // Uncrouch
        ToggleCrouch(false);

        // Start the jump
        animator.Play("Jump Start");
        
        // Disable other animations if playing
        animator.SetBool("Dance", false);
        animator.SetBool("Backflip", false);

        // Apply rigidbody speed
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    // Toggles animator and crouching variable
    private void ToggleCrouch(bool toggle)
    {
        animator.SetBool("Crouching", toggle);
        isCrouching = toggle;
    }
    
    // Change working cameras
    private void OnCameraChange()
    {
        // Swap cameras
        thirdPersonCamera.gameObject.SetActive(!thirdPersonCamera.gameObject.activeSelf);
        firstPersonCamera.gameObject.SetActive(!firstPersonCamera.gameObject.activeSelf);
        
        // Change reticule render camera
        if (thirdPersonCamera.gameObject.activeSelf) playerReticule.worldCamera = thirdPersonCamera;
        else playerReticule.worldCamera = firstPersonCamera;
    }

    // Grounded checks (stupid)
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IInteractable interactable)) interactable.Interact(gameObject);
        else isGrounded = true; // If not an interactable, then its just ground ig
    }

    private void OnTriggerExit(Collider other) { if (!other.TryGetComponent(out IInteractable _)) isGrounded = false; }
}