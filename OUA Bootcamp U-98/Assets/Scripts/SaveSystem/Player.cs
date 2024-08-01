using UnityEngine;

public class Player : MonoBehaviour
{
    public float health;
    public float mana;
    public Vector3 position;
    public Quaternion rotation;
    public bool isInteracting;
    public bool isDead;
    public bool isHit;
    public Vector2 movementInput;
    public Vector2 cameraInput;
    public float moveAmount;
    public float verticalInput;
    public float horizontalInput;
    public bool jumpInput;
    public bool dodgeInput;
    public bool attackInput;
    public bool sprintInput;
    public bool inventoryInput;
    public bool interactInput;
    public bool isGrounded;
    public bool isJumping;
    public bool isDodging;
    public bool isAttacking;
    public bool isSprinting;

    public void SavePlayerData(PlayerManager playerManager, PlayerHealth playerHealth, InputManager inputManager, CharController charController)
    {
        health = playerHealth.currentHealth;
        mana = 0f; // If you have a mana system, replace this with actual value
        position = transform.position;
        rotation = transform.rotation;
        isInteracting = playerManager.isInteracting;
        isDead = playerHealth.isDead;
        isHit = playerHealth.isHit;
        movementInput = inputManager.movementInput;
        cameraInput = inputManager.cameraInput;
        moveAmount = inputManager.moveAmount;
        verticalInput = inputManager.verticalInput;
        horizontalInput = inputManager.horizontalInput;
        jumpInput = inputManager.jumpInput;
        dodgeInput = inputManager.dodgeInput;
        attackInput = inputManager.attackInput;
        sprintInput = inputManager.sprintInput;
        inventoryInput = inputManager.inventoryInput;
        interactInput = inputManager.interactInput;
        isGrounded = charController.isGrounded;
        isJumping = charController.isJumping;
        isDodging = charController.isDodging;
        isAttacking = charController.isAttacking;
        isSprinting = charController.isSprinting;
    }

    public void LoadPlayerData(PlayerManager playerManager, PlayerHealth playerHealth, InputManager inputManager, CharController charController)
    {
        playerHealth.currentHealth = health;
        // mana = mana; // Load your mana system here if applicable
        transform.position = position;
        transform.rotation = rotation;
        playerManager.isInteracting = isInteracting;
        playerHealth.isDead = isDead;
        playerHealth.isHit = isHit;
        inputManager.movementInput = movementInput;
        inputManager.cameraInput = cameraInput;
        inputManager.moveAmount = moveAmount;
        inputManager.verticalInput = verticalInput;
        inputManager.horizontalInput = horizontalInput;
        inputManager.jumpInput = jumpInput;
        inputManager.dodgeInput = dodgeInput;
        inputManager.attackInput = attackInput;
        inputManager.sprintInput = sprintInput;
        inputManager.inventoryInput = inventoryInput;
        inputManager.interactInput = interactInput;
        charController.isGrounded = isGrounded;
        charController.isJumping = isJumping;
        charController.isDodging = isDodging;
        charController.isAttacking = isAttacking;
        charController.isSprinting = isSprinting;
    }
}
