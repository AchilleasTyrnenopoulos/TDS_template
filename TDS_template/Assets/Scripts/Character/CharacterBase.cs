using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class CharacterBase : MonoBehaviour
{
    //[SerializeField] private GameObject _characterModel;
    //[SerializeField] private Animator _characterAnimator;
    [SerializeField] private GameObject _cameraTarget;
    [SerializeField] private CharacterAbility[] _characterAbilities;

    private void Awake()
    {
        _cameraTarget.name = Strings.CameraTarget;        
    }

    private void Update()
    {

    }

}

