using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float health;
    public Vector2 movementInput;
    public Vector2 cameraInput;
    public float verticalInput;
    public float horizontalInput;
    public bool sprintInput;
    public bool inventoryInput;
    public bool interactInput;
    public bool isGrounded;
    public bool isSprinting;

    public void SavePlayerData(PlayerManager playerManager, PlayerHealth playerHealth, InputManager inputManager, CharController charController)
    {
        health = playerHealth.currentHealth;
        movementInput = inputManager.movementInput;
        cameraInput = inputManager.cameraInput;
        verticalInput = inputManager.verticalInput;
        horizontalInput = inputManager.horizontalInput;
        sprintInput = inputManager.sprintInput;
        inventoryInput = inputManager.inventoryInput;
        interactInput = inputManager.interactInput;
        isGrounded = charController.isGrounded;
        isSprinting = charController.isSprinting;

        Debug.Log("Player data saved.");
    }

    public void LoadPlayerData(PlayerManager playerManager, PlayerHealth playerHealth, InputManager inputManager, CharController charController)
    {
        playerHealth.currentHealth = health;
        inputManager.movementInput = movementInput;
        inputManager.cameraInput = cameraInput;
        inputManager.verticalInput = verticalInput;
        inputManager.horizontalInput = horizontalInput;
        inputManager.sprintInput = sprintInput;
        inputManager.inventoryInput = inventoryInput;
        inputManager.interactInput = interactInput;
        charController.isGrounded = isGrounded;
        charController.isSprinting = isSprinting;

        Debug.Log("Player data loaded.");
    }
}
