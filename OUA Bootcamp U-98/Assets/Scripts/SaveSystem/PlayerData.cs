using System.Collections;
using System.Collections.Generic;
using UnityEngine; // Bu satýr eksik olabilir

[System.Serializable]
public class PlayerData
{
    public float health;
    public float mana;
    public Vector3 position; // Vector3 türü burada kullanýlacak
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
}
