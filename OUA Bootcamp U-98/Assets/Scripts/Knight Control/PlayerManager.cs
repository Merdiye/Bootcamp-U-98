using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    Animator animator;
    InputManager inputManager;
    CharController charController;
    CameraManager cameraManager;
    PlayerHealth playerHealth;
    public bool isInteracting;

    private void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();
        animator = GetComponent<Animator>();
        inputManager = GetComponent<InputManager>();
        cameraManager = FindObjectOfType<CameraManager>();
        charController = GetComponent<CharController>();
    }

    private void Update()
    {
        if(playerHealth.isDead) return;
        // Handle all player inputs
        inputManager.HandleAllInputs();
    }

    private void FixedUpdate()
    {
        if (playerHealth.isDead ) return;
        // Handle all movement related updates
        charController.HandleAllMovement();
    }

    private void LateUpdate()
    {
        if (playerHealth.isDead) return;
        // Handle all camera related updates
        cameraManager.HandleAllCameraMovement();

        // Update interaction and state flags
        UpdateAnimatorStates();
    }

    private void UpdateAnimatorStates()
    {
        isInteracting = animator.GetBool("isInteracting");
        charController.isDodging = animator.GetBool("isDodging");
        charController.isJumping = animator.GetBool("isJumping");
        charController.isAttacking = animator.GetBool("isAttacking");
        animator.SetBool("isGrounded", charController.isGrounded);
    }
}
