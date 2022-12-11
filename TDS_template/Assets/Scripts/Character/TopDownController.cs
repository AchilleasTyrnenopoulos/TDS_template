using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownController : MonoBehaviour
{
    private CharacterController _characterController;
    private CollisionFlags _collisionFlags;
    private Vector3 _motion;
    private Vector3 _newVelocity;
    private Vector3 _horizontalVelocityDelta;
    private float _stickyOffset = 0f;
    public Vector3 velocity;
    public Vector3 currentMovement;

    private void Awake()
    {
        //cache the CharacterContoller component
        _characterController = this.gameObject.GetComponent<CharacterController>();
    }

    public void SetMovement(Vector3 movement)
    {
        currentMovement = movement;
    }

    private void Update()
    {
        //_newVelocity = velocity;

        _newVelocity = currentMovement;

        ComputeVelocity();
        MoveCharacterController();
        //ComputeNewVelocity();
    }

    private void ComputeVelocity()
    {
        _motion = _newVelocity * Time.deltaTime;
        _horizontalVelocityDelta.x = _motion.x;
        _horizontalVelocityDelta.y = 0f;
        _horizontalVelocityDelta.z = _motion.z;
        _stickyOffset = Mathf.Max(_characterController.stepOffset, _horizontalVelocityDelta.magnitude);

        _motion -= _stickyOffset * Vector3.up;
    }

    //private void ComputeNewVelocity()
    //{
    //    velocity = _newVelocity;
    //}

    private void MoveCharacterController()
    {
        Debug.Log(_motion);
        _collisionFlags = _characterController.Move(_motion);
    }
    
}
