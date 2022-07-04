using System;
using UnityEngine;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(Animator))]
public class PirateAnimator : MonoBehaviour
{
    private Movement _movement;
    private Animator _animator;

    //private void Start()
    //{
    //    _movement = GetComponent<Movement>();
    //    _animator = GetComponent<Animator>();
    //    _movement.StateChanged.AddListener(ChangeAnimation);
    //}

    //private void ChangeAnimation()
    //{
    //    Debug.Log($"Changing animation to " + state.ToString());
    //    _animator.SetTrigger(state.ToString());
    //}
}
