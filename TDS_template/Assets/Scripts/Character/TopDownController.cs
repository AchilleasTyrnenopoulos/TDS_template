using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownController : MonoBehaviour
{
    [Header("Gravity")]

    [SerializeField] private float _gravity = 40f;
    [SerializeField] private bool _grounded = false;
    [SerializeField] private bool _justGotGrounded = false;
    [SerializeField] private bool _groundedLastFrame = false;
    private Vector3 _newVelocity;
    [SerializeField] private Vector3 _velocity;
    [SerializeField] private float _maximumFallSpeed = 20f;
    [SerializeField] private Vector3 _addedForce;

    private void Update()
    {
        //check if grounded
        _justGotGrounded = (!_groundedLastFrame && _grounded);
        _groundedLastFrame = _grounded;

    }

    private void FixedUpdate()
    {
        _newVelocity = _velocity;

        AddGravity();
    }

    private void AddGravity()
    {
        if(_grounded)
        {
            _newVelocity.y = Mathf.Min(0, _newVelocity.y) - _gravity * Time.deltaTime;
        }
        else
        {
            _newVelocity.y = _velocity.y - _gravity * Time.deltaTime;
            _newVelocity.y = Mathf.Max(_newVelocity.y, -_maximumFallSpeed);
        }

        _newVelocity += _addedForce;
        _addedForce = Vector3.zero;
    }
}
