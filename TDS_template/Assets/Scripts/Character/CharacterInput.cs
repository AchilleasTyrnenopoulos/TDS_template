using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterInput : MonoBehaviour
{
    private float _horizontalInput;
    private float _verticalInput;

    public void OnMovement(InputValue value)
    {
        _horizontalInput = value.Get<Vector2>().x;
        _verticalInput = value.Get<Vector2>().y;

        Debug.Log($"horizontal movement: {_horizontalInput}\nvertical movement: {_verticalInput}");
    }

    public float GetHorizontalInput() => _horizontalInput;

    public float GetVerticalInput() => _verticalInput;
}
