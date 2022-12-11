using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : CharacterAbility
{
    [SerializeField] private float _movementSpeed;
    private Vector3 _movementVector;
    private Vector2 _currentInput = Vector2.zero;
    private Vector2 _normalizedInput;    
    private float _horizontalMovement;
    private float _verticalMovement;

    public override void ProcessAbility()
    {
        HandleInput();

        HandleMovement();
    }

    private void HandleInput()
    {
        _horizontalMovement = _input.GetHorizontalInput();
        _verticalMovement = _input.GetVerticalInput();
    }

    private void HandleMovement()
    {
        SetMovement();
    }

    private void SetMovement()
    {
        //reset _movement vector & current input
        _movementVector = Vector3.zero;
        _currentInput = Vector2.zero;

        //set current inpur x and y depending on the input 
        _currentInput.x = _horizontalMovement;
        _currentInput.y = _verticalMovement;

        //normalize the input
        _normalizedInput = _currentInput.normalized;

        //set the movement vector
        _movementVector.x = _normalizedInput.x;
        _movementVector.y = 0f;
        _movementVector.z = _normalizedInput.y;

        //multiply the movement vector with the movement speed
        _movementVector *= _movementSpeed;

        _controller.SetMovement(_movementVector);       
    }
}
