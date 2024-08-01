using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

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
        Debug.Log("Starting SavePlayer.");

        if (player != null)
        {
            PlayerManager playerManager = player.GetComponent<PlayerManager>();
            PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
            InputManager inputManager = player.GetComponent<InputManager>();
            CharController charController = player.GetComponent<CharController>();

            if (playerManager != null && playerHealth != null && inputManager != null && charController != null)
            {
                player.SavePlayerData(playerManager, playerHealth, inputManager, charController);

                PlayerData data = new PlayerData
                {
                    health = player.health,
                    position = new SerializableVector3(player.transform.position),
                    rotation = new SerializableVector3(player.transform.eulerAngles),
                    movementInput = new SerializableVector2(player.movementInput),
                    cameraInput = new SerializableVector2(player.cameraInput),
                    verticalInput = player.verticalInput,
                    horizontalInput = player.horizontalInput,
                    sprintInput = player.sprintInput,
                    inventoryInput = player.inventoryInput,
                    interactInput = player.interactInput,
                    isGrounded = player.isGrounded,
                    isSprinting = player.isSprinting,
                };

                // Envanteri kaydet
                data.inventoryItems = new List<SerializableItem>();
                foreach (Item item in InventoryManager.Instance.items)
                {
                    data.inventoryItems.Add(new SerializableItem(item));
                }

                BinaryFormatter formatter = new BinaryFormatter();
                string path = Application.persistentDataPath + "/savefile.fun";
                using (FileStream stream = new FileStream(path, FileMode.Create))
                {
                    formatter.Serialize(stream, data);
                }

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
        Debug.Log("Starting LoadPlayer.");

        string path = Application.persistentDataPath + "/savefile.fun";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            using (FileStream stream = new FileStream(path, FileMode.Open))
            {
                PlayerData data = formatter.Deserialize(stream) as PlayerData;

                if (player != null)
                {
                    player.health = data.health;
                    player.transform.position = data.position.ToVector3();
                    player.transform.eulerAngles = data.rotation.ToVector3();
                    player.movementInput = data.movementInput.ToVector2();
                    player.cameraInput = data.cameraInput.ToVector2();
                    player.verticalInput = data.verticalInput;
                    player.horizontalInput = data.horizontalInput;
                    player.sprintInput = data.sprintInput;
                    player.inventoryInput = data.inventoryInput;
                    player.interactInput = data.interactInput;
                    player.isGrounded = data.isGrounded;
                    player.isSprinting = data.isSprinting;

                    // Envanteri yükle
                    InventoryManager.Instance.CleanContent();
                    InventoryManager.Instance.items.Clear();
                    foreach (SerializableItem itemData in data.inventoryItems)
                    {
                        Item item = ScriptableObject.CreateInstance<Item>();
                        item.id = itemData.id;
                        item.itemName = itemData.itemName;
                        item.value = itemData.value;
                        InventoryManager.Instance.Add(item);
                    }
                    InventoryManager.Instance.ListItems();

                 

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
        }
    }
}
