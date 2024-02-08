using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float sensibility = 18f;
    public float playerSpeed = 120f;
    public float cameraFov = 60f;
    public float walkingVelocityCap = 4f;
    public float fallingVelocityCap = 5f;

    private Animator animator;
    private Vector2 cameraMovement;
    private Vector2 playerMovement;

    // Setting player stuff up
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Camera.main.fieldOfView = cameraFov;
        animator = transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateLookingPosition();
    }


    // Physics simulations on FixedUpdate
    void FixedUpdate()
    {
        MovePlayer();
    }

    // Move camera
    private void UpdateLookingPosition()
    {
        // Calculate how much to rotate
        transform.Rotate(sensibility * cameraMovement.x * Time.deltaTime * Vector3.up);
    }

    // Moves the player
    private void MovePlayer()
    {
        transform.Translate(playerMovement.y * playerSpeed * Vector3.forward);
        transform.Translate(playerMovement.x * playerSpeed * Vector3.right);
    }

    // Get the player's camera input
    private void OnLook(InputValue ctx) { cameraMovement = ctx.Get<Vector2>(); }

    // Get the player's movement input
    private void OnMove(InputValue ctx) { playerMovement = ctx.Get<Vector2>(); }

    // Dance
    private void OnDance()
    {
        if (animator.GetBool("Jump")) return;
        animator.SetBool("Dance", !animator.GetBool("Dance"));
    }

    // Backflip
    private void OnBackflip()
    {
        if (animator.GetBool("Jump")) return;
        animator.SetBool("Backflip", !animator.GetBool("Backflip"));
    }

    // Jump
    private void OnJump()
    {
        animator.SetBool("Jump", !animator.GetBool("Jump"));
        animator.SetBool("Dance", false);
        animator.SetBool("Backflip", false);
    }
}