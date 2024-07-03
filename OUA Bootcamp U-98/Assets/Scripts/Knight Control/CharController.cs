using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    AnimatorManager animatorManager;
    PlayerManager playerManager;
    InputManager inputManager;
    DodgeSkill dodgeSkill;
    Vector3 moveDirection;
    Transform cameraObject;
    public Rigidbody playerRigidbody;

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
    public bool isDodging;
    public bool isCanDodge;
    public bool isOnStairs;

    [Header("Speeds")]
    public float movementSpeed = 7f;
    public float stairMovementSpeed = 10f;
    public float rotationSpeed = 15f;
    public float dodgeSpeed = 10f;


    private void Awake()
    {
        dodgeSkill = GetComponent<DodgeSkill>();
        isCanDodge = true;
        animatorManager = GetComponent<AnimatorManager>();
        playerManager = GetComponent<PlayerManager>();
        inputManager = GetComponent<InputManager>();
        playerRigidbody = GetComponent<Rigidbody>();
        cameraObject = Camera.main.transform;
    }

    private void HandleMovement()
    {
        if (isJumping || isDodging)
            return;

        moveDirection = cameraObject.forward * inputManager.verticalInput + cameraObject.right * inputManager.horizontalInput;
        moveDirection.Normalize();
        moveDirection.y = 0;

        float currentSpeed = isOnStairs ? stairMovementSpeed : movementSpeed;
        moveDirection *= currentSpeed;

        if (isGrounded)
        {
            Vector3 movementVelocity = moveDirection;
            if (isOnStairs && (inputManager.verticalInput != 0 || inputManager.horizontalInput != 0))
            {
                movementVelocity.y = playerRigidbody.velocity.y;
            }
            playerRigidbody.velocity = movementVelocity;
        }
    }

    private void HandleRotation()
    {
        if (isJumping || isDodging)
            return;

        Vector3 targetDirection = cameraObject.forward * inputManager.verticalInput + cameraObject.right * inputManager.horizontalInput;
        targetDirection.Normalize();
        targetDirection.y = 0;

        if (targetDirection == Vector3.zero)
            targetDirection = transform.forward;

        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    public void HandleAllMovement()
    {
        HandleFallingAndLanding();
        if (playerManager.isInteracting && !isGrounded)
            return;

        HandleMovement();
        HandleRotation();
    }

    private void HandleFallingAndLanding()
    {
        RaycastHit hit;
        Vector3 raycastOrigin = transform.position + Vector3.up * raycastHeightOffset;
        Vector3 targetPosition = transform.position;

        float currentMaxDistance = isJumping ? maxDistance : stairMaxDistance;

        if (!isGrounded && !isJumping)
        {
            if (!playerManager.isInteracting)
            {
                animatorManager.PlayTargetAnimation("Falling", true);
            }
            animatorManager.animator.SetBool("isDodging", false);

            inAirTimer += Time.deltaTime;
            playerRigidbody.AddForce(transform.forward * leapingVelocity);
            playerRigidbody.AddForce(Vector3.down * fallingVelocity * inAirTimer);
        }

        if (Physics.SphereCast(raycastOrigin, 0.2f, Vector3.down, out hit, currentMaxDistance, groundLayer))
        {
            if (!isGrounded && playerManager.isInteracting)
            {
                animatorManager.PlayTargetAnimation("Landing", true);
            }

            targetPosition.y = hit.point.y;
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

        if (isGrounded && !isJumping)
        {
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime / 0.1f);
        }
    }

    public void HandleJumping()
    {
        if (isGrounded && !isDodging)
        {
            animatorManager.animator.SetBool("isJumping", true);
            animatorManager.PlayTargetAnimation("Jumping", true);

            float jumpingVelocity = Mathf.Sqrt(-2 * gravityIntensity * jumpHeight);
            Vector3 playerVelocity = moveDirection;
            playerVelocity.y = jumpingVelocity;
            playerRigidbody.velocity = playerVelocity;
        }
    }

    public void HandleDodge()
    {
        if (!isGrounded || !isCanDodge)
            return;
        isCanDodge = false;
        animatorManager.animator.SetBool("isDodging", true);
        animatorManager.PlayTargetAnimation("Dodge", true);

        Vector3 dodgeDirection = -transform.forward; // Karakterin arkasýna doðru yön
        Vector3 dodgeVelocity = dodgeDirection * dodgeSpeed;
        playerRigidbody.velocity = dodgeVelocity;

        StartCoroutine(EndDodge());
        StartCoroutine(CooldownDodge());

    }

    private IEnumerator EndDodge()
    {
        yield return new WaitForSeconds(0.5f); // Dodge animasyonunun süresi kadar bekleyin
        animatorManager.animator.SetBool("isDodging", false);
    }

    private IEnumerator CooldownDodge()
    {
        dodgeSkill.isDodging = true;
        yield return new WaitForSeconds(2f);
        isCanDodge = true;
    }
}




