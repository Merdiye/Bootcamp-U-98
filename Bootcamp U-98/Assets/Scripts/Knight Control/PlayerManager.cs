using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    InputManager inputManager;
    CharController charController;
    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        charController = GetComponent<CharController>();
    }

    private void Update()
    {
        inputManager.HandleAllInputs();
    }

    private void FixedUpdate()
    {
        charController.HandleAllMovement();
    }
}
