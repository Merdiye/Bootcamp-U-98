using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    AnimatorManager animatorManager;
    PlayerManager playerManager;
    InputManager inputManager;
    Vector3 moveDirection;
    Transform cameraObject;
    Rigidbody playerRigidbody;

    [Header("Falling")]
    public float inAirTimer;
    public float fallingVelocity;
    public float leapingVelocity = 2f;
    public float raycastHeightOffset = 0.5f;
    public LayerMask groundLayer;
    public float maxDistance = 0.6f;
    public float stairMaxDistance = 1.2f;

    [Header("Jumping")]
    public float gravityIntensity = -10;
    public float jumpHeight = 2f;

    [Header("Flags")]
    public bool isGrounded;
    public bool isJumping;
    public bool isOnStairs;

    [Header("Speeds")]
    public float movementSpeed = 7f;
    public float stairMovementSpeed = 10f;
    public float rotationSpeed = 15f;

    private void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();
        playerManager = GetComponent<PlayerManager>();
        inputManager = GetComponent<InputManager>();
        playerRigidbody = GetComponent<Rigidbody>();
        cameraObject = Camera.main.transform;
    }

    private void HandleMovement()
    {
        if (isJumping)
            return;

        moveDirection = cameraObject.forward * inputManager.verticalInput;
        moveDirection += cameraObject.right * inputManager.horizontalInput;
        moveDirection.Normalize();
        moveDirection.y = 0;

        float currentSpeed = isOnStairs ? stairMovementSpeed : movementSpeed;
        moveDirection = moveDirection * currentSpeed;

        if (isGrounded && !isJumping)
        {
            Vector3 movementVelocity = moveDirection;
            if (isOnStairs)
            {
                movementVelocity.y = playerRigidbody.velocity.y;
            }
            playerRigidbody.velocity = movementVelocity;
        }
    }

    private void HandleRotation()
    {
        if (isJumping)
            return;

        Vector3 targetDirection = Vector3.zero;
        targetDirection = cameraObject.forward * inputManager.verticalInput;
        targetDirection += cameraObject.right * inputManager.horizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0;

        if(targetDirection == Vector3.zero)
            targetDirection = transform.forward;

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        Quaternion playerRotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        if (isGrounded && !isJumping)
        {
            transform.rotation = playerRotation;
        }
    }

    public void HandleAllMovement()
    {
        HandleFallingAndLanding();
        if (playerManager.isInteracting && !isGrounded)
        {
            return;
        }
        HandleMovement();
        HandleRotation();
    }

    private void HandleFallingAndLanding()
    {
        RaycastHit hit;
        Vector3 raycastOrigin = transform.position;
        Vector3 targetPosition;
        raycastOrigin.y += raycastHeightOffset; 
        targetPosition = transform.position;

        float currentMaxDistance = isJumping ? maxDistance : stairMaxDistance;

        if(!isGrounded && !isJumping)
        {
            if (!playerManager.isInteracting)
            {
                animatorManager.PlayTargetAnimation("Falling", true);
            }
            inAirTimer += Time.deltaTime;
            playerRigidbody.AddForce(transform.forward * leapingVelocity);
            playerRigidbody.AddForce(-Vector3.up * fallingVelocity * inAirTimer);
        }

        if(Physics.SphereCast(raycastOrigin, 0.2f, -Vector3.up, out hit, currentMaxDistance, groundLayer))
        {
            if(!isGrounded && playerManager.isInteracting)
            {
                animatorManager.PlayTargetAnimation("Landing", true);
            }

            Vector3 raycastHitPoint = hit.point;
            targetPosition.y = raycastHitPoint.y;

            inAirTimer = 0;
            isGrounded = true;
            playerManager.isInteracting = false;
            isOnStairs = hit.collider.CompareTag("Stairs");

        }
        else
        {
            isGrounded = false;
            isOnStairs = false;
        }

        if(isGrounded && !isJumping)
        {
            if(!playerManager.isInteracting || inputManager.moveAmount > 0)
            {
                transform.position = Vector3.Lerp(transform.position ,targetPosition , Time.deltaTime / 0.1f);
            }
            else
            {
                transform.position = targetPosition;
            }
        }
    }

    public void HandleJumping()
    {
        if(isGrounded)
        {
            animatorManager.animator.SetBool("isJumping", true);
            animatorManager.PlayTargetAnimation("Jumping", false);

            float jumpingVelocity = Mathf.Sqrt(-2 * gravityIntensity * jumpHeight);
            Vector3 playerVelocity = moveDirection;
            playerVelocity.y = jumpingVelocity;
            playerRigidbody.velocity = playerVelocity;
        }
    }
}


