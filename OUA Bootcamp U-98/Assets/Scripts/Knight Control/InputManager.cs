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

    private void Awake()
    {
        animatorManager = GetComponent<AnimatorManager>();
        charController = GetComponent<CharController>();
    }

    private void OnEnable()
    {
        if(charInput == null)
        {
            charInput = new CharacterInput();
            charInput.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            charInput.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
            charInput.PlayerActions.Jump.performed += i => jumpInput = true;

        }
        charInput.Enable();
    }

    private void OnDisable()
    {
        charInput.Disable();
    }

    public void HandleAllInputs()
    {
        HandleMovementInput();
        HandleJumpingInput();
    }

    private void HandleMovementInput()
    {
        verticalInput = movementInput.y;
        horizontalInput = movementInput.x;

        cameraInputX = cameraInput.x;
        cameraInputY = cameraInput.y;

        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontalInput) + Mathf.Abs(verticalInput)); 
        animatorManager.UpdateAnimatorValues(0f, moveAmount);
    }

    private void HandleJumpingInput()
    {
        if (jumpInput)
        {
            jumpInput = false;
            charController.HandleJumping();
        }
    }
}
