using UnityEngine;
using UnityEngine.Events;

public abstract class AnimationEventSource : MonoBehaviour
{
    public enum State
    {
        Idle,
        Running,
        Jumping
    }
    public abstract UnityEvent<State> StateNameChanged { get; }
}
