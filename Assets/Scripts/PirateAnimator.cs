using UnityEngine;

[RequireComponent(typeof(AnimationEventSource))]
[RequireComponent(typeof(Animator))]
public class PirateAnimator : MonoBehaviour
{
    private Animator _animator;
    private AnimationEventSource _character;
    private AnimationEventSource.State _state = AnimationEventSource.State.Idle;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _character = GetComponent<AnimationEventSource>();
        _character.StateNameChanged.AddListener(ChangeAnimation);
    }

    public void ChangeAnimation(AnimationEventSource.State state)
    {
        _animator.ResetTrigger(_state.ToString());
        _animator.SetTrigger(state.ToString());
        _state = state;
    }

    private void OnDisable()
    {
        _character.StateNameChanged.RemoveListener(ChangeAnimation);
    }
}
