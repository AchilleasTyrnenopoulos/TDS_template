using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAbility : MonoBehaviour
{
    protected TopDownController _controller;
    protected CharacterInput _input;

    private void Awake()
    {
        //cache components
        _controller = this.gameObject.GetComponent<TopDownController>();
        _input = this.gameObject.GetComponent<CharacterInput>();
    }

    public virtual void ProcessAbility() { }
}
