using System;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Get input and process input
/// </summary>
public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput _input;
    private InputActionAsset _inputActionMap;
    public Vector2 MovementInput;
    public int InputX;
    public int InputY;
    public bool JumpInput;
    // TODO: run and crouch
    // public bool RunInput;
    // public bool CrouchInput; 

    private void Awake()
    {
        _input = GetComponent<PlayerInput>();
        _inputActionMap = _input.actions;
    }

    private void Start()
    {
        _inputActionMap.Enable();
    }

    public void OnMoveInput(InputAction.CallbackContext context)
    {
        MovementInput = context.ReadValue<Vector2>();
        InputX = (int)(MovementInput * Vector2.right).normalized.x;
        InputY = (int)(MovementInput * Vector2.up).normalized.y;
    }

    public void OnJumpInput(InputAction.CallbackContext context)
    {
        if (context.started)
            JumpInput = true;
    }

    public void UseJumpInput() => JumpInput = false;
}