using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public CharacterInput charInput;
    public Vector2 movementInput;

    private void OnEnable()
    {
        if(charInput == null)
        {
            charInput = new CharacterInput();
            charInput.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();

        }
        charInput.Enable();
    }

    private void OnDisable()
    {
        charInput.Disable();
    }
}
