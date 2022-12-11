using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AT.TDS
{
    public class CharacterBase : MonoBehaviour
    {
        //[SerializeField] private GameObject _characterModel;
        //[SerializeField] private Animator _characterAnimator;
        [SerializeField] private GameObject _cameraTarget;

        private void Awake()
        {
            _cameraTarget.name = Strings.CameraTarget;
        }
    }
}
