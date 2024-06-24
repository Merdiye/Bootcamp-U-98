using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    InputManager inputManager;
    CharController charController;
    CameraManager cameraManager;
    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        cameraManager = FindObjectOfType<CameraManager>();
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

    private void LateUpdate()
    {
        cameraManager.HandleAllCameraMovement();
    }
}
