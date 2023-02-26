using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    private InputActions _input;

    private Vector2 _moveInput;
    private bool _isMoving = false;

    private void OnEnable()
    {
        _input = new InputActions();

        _input.Player.Enable();

        _input.Player.Move.started += StartedMoving;
        _input.Player.Move.performed += SetMoveInput;
        _input.Player.Move.canceled += SetMoveInput;
        _input.Player.Move.canceled += StoppedMoving;
    }

    private void OnDisable()
    {
        _input.Player.Move.started -= StartedMoving;
        _input.Player.Move.performed -= SetMoveInput;
        _input.Player.Move.canceled -= SetMoveInput;
        _input.Player.Move.canceled -= StoppedMoving;

        _input.Player.Disable();
    }

    public Vector3 GetMoveInput()
    {
        return new Vector3(_moveInput.x, 0f, _moveInput.y);
    }

    private void SetMoveInput(InputAction.CallbackContext ctx)
    {
        _moveInput = ctx.ReadValue<Vector2>();

    }
    public bool GetIsMoving() => _isMoving;    

    private void StartedMoving(InputAction.CallbackContext ctx)
    {
        _isMoving = true;
    }

    private void StoppedMoving(InputAction.CallbackContext ctx)
    {
        _isMoving = false;
    }

}
