using UnityEngine;
using System.IO;

public class SaveSystem : MonoBehaviour
{
    public Player player;

    private void Start()
    {
        if (player == null)
        {
            GameObject playerObject = GameObject.FindWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.GetComponent<Player>();
                if (player == null)
                {
                    Debug.LogError("The object with tag 'Player' does not have a Player component.");
                }
            }
            else
            {
                Debug.LogError("Player object not found in the scene with tag 'Player'.");
            }
        }
    }

    public void SavePlayer()
    {
        if (player != null)
        {
            PlayerManager playerManager = player.GetComponent<PlayerManager>();
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            InputManager inputManager = player.GetComponent<InputManager>();
            CharController charController = player.GetComponent<CharController>();

            if (playerManager != null && playerHealth != null && inputManager != null && charController != null)
            {
                player.SavePlayerData(playerManager, playerHealth, inputManager, charController);
                string path = Application.persistentDataPath + "/savefile.json";
                PlayerData data = new PlayerData
                {
                    health = player.health,
                    mana = player.mana,
                    position = player.position,
                    rotation = player.rotation,
                    isInteracting = player.isInteracting,
                    isDead = player.isDead,
                    isHit = player.isHit,
                    movementInput = player.movementInput,
                    cameraInput = player.cameraInput,
                    moveAmount = player.moveAmount,
                    verticalInput = player.verticalInput,
                    horizontalInput = player.horizontalInput,
                    jumpInput = player.jumpInput,
                    dodgeInput = player.dodgeInput,
                    attackInput = player.attackInput,
                    sprintInput = player.sprintInput,
                    inventoryInput = player.inventoryInput,
                    interactInput = player.interactInput,
                    isGrounded = player.isGrounded,
                    isJumping = player.isJumping,
                    isDodging = player.isDodging,
                    isAttacking = player.isAttacking,
                    isSprinting = player.isSprinting
                };

                string json = JsonUtility.ToJson(data);
                File.WriteAllText(path, json);

                Debug.Log("Game Saved!");
            }
            else
            {
                Debug.LogError("One or more required components are missing from the Player object.");
            }
        }
        else
        {
            Debug.LogError("Player reference is missing.");
        }
    }

    public void LoadPlayer()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);

            if (player != null)
            {
                player.health = data.health;
                player.mana = data.mana;
                player.position = data.position;
                player.rotation = data.rotation;
                player.isInteracting = data.isInteracting;
                player.isDead = data.isDead;
                player.isHit = data.isHit;
                player.movementInput = data.movementInput;
                player.cameraInput = data.cameraInput;
                player.moveAmount = data.moveAmount;
                player.verticalInput = data.verticalInput;
                player.horizontalInput = data.horizontalInput;
                player.jumpInput = data.jumpInput;
                player.dodgeInput = data.dodgeInput;
                player.attackInput = data.attackInput;
                player.sprintInput = data.sprintInput;
                player.inventoryInput = data.inventoryInput;
                player.interactInput = data.interactInput;
                player.isGrounded = data.isGrounded;
                player.isJumping = data.isJumping;
                player.isDodging = data.isDodging;
                player.isAttacking = data.isAttacking;
                player.isSprinting = data.isSprinting;

                PlayerManager playerManager = player.GetComponent<PlayerManager>();
                PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
                InputManager inputManager = player.GetComponent<InputManager>();
                CharController charController = player.GetComponent<CharController>();

                if (playerManager != null && playerHealth != null && inputManager != null && charController != null)
                {
                    player.LoadPlayerData(playerManager, playerHealth, inputManager, charController);

                    Debug.Log("Game Loaded!");
                }
                else
                {
                    Debug.LogError("One or more required components are missing from the Player object.");
                }
            }
            else
            {
                Debug.LogError("Player reference is missing.");
            }
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
        }
    }
}
