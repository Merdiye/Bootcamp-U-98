using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public CharacterInput charInput;
    AnimatorManager animatorManager;
    CharController charController;

    public Vector2 movementInput;
    public Vector2 cameraInput;

    public float cameraInputX;
    public float cameraInputY;

    public float moveAmount;
    public float verticalInput;
    public float horizontalInput;

    public bool jumpInput;
    public bool dodgeInput;
    public bool attackInput;
    public bool sprintInput;
    public bool inventoryInput;
    public bool interactInput;

    private void Awake()
    {
        inventoryInput = false;
        animatorManager = GetComponent<AnimatorManager>();
        charController = GetComponent<CharController>();
    }

    private void OnEnable()
    {
        if(charInput == null)
        {

            charInput = new CharacterInput();
            charInput.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
            charInput.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            charInput.PlayerActions.Jump.performed += i => jumpInput = true;
            charInput.PlayerActions.Dodge.performed += i => dodgeInput = true;
            charInput.PlayerActions.Attack.performed += i => attackInput = true;
            charInput.PlayerActions.Sprint.performed += i => sprintInput = true;
            charInput.PlayerActions.Sprint.canceled += i => sprintInput = false;
            charInput.PlayerActions.InventoryButton.performed += i => inventoryInput = true;
            charInput.PlayerActions.Interact.performed += i => interactInput = true;

        }
        charInput.Enable();
    }

    private void OnDisable()
    {
        charInput.Disable();
    }

    public void HandleAllInputs()
    {
        HandleAttackInput();
        HandleMovementInput();
        HandleJumpingInput();
        HandleDodgeInput();
        HandleSprintInput();
        HandleInventoryInput();
        HandleInteractInput();
    }

    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        cameraInputX = cameraInput.x;
        cameraInputY = cameraInput.y;

        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput)); 
        animatorManager.UpdateAnimatorValues(0f, moveAmount, charController.isSprinting);
    }

    private void HandleJumpingInput()
    {
        if (jumpInput)
        {
            jumpInput = false;
            charController.HandleJumping();
        }
    }

    private void HandleDodgeInput()
    {
        if(dodgeInput)
        {
            dodgeInput = false;
            charController.HandleDodge();
        }
    }

    private void HandleAttackInput()
    {
        if (attackInput)
        {
            attackInput = false;
            charController.HandleAttack();
        }
    }

    private void HandleSprintInput()
    {
        if (sprintInput && moveAmount >= 0.5f)
        {
            charController.isSprinting = true;
        }
        else 
        {
            charController.isSprinting = false;
        }
    }

    private void HandleInventoryInput()
    {
        if (inventoryInput)
        {
            Debug.Log("ohh listele pa�am listele");
            charController.OpenInventory();
            inventoryInput = false;
        }
    }

    private void HandleInteractInput()
    {
        if (interactInput)
        {
            charController.Interact();
            interactInput = false;
        }
    }
}
