using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    public float health;
    public SerializableVector3 position;
    public SerializableVector3 rotation;
    public SerializableVector2 movementInput;
    public SerializableVector2 cameraInput;
    public float verticalInput;
    public float horizontalInput;
    public bool sprintInput;
    public bool inventoryInput;
    public bool interactInput;
    public bool isGrounded;
    public bool isSprinting;

    // Envanter bilgileri
    public List<SerializableItem> inventoryItems = new List<SerializableItem>();

    // Görev bilgileri
    public float currentKill;
    public float targetKill;
}
