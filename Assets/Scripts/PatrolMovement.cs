using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class PatrolMovement : AnimationEventSource
{
    [SerializeField] private float _idleDuration = 5f;
    [SerializeField] private float _travelDuration = 5f;
    [SerializeField] private Transform _path;

    private float _idleRunningDuration = 0f;
    private float _travelRunningDuration = 0f;
    private int _currentWaypointIndex = 0;
    private Rigidbody2D _rigidbody;
    private State _state = State.Idle;
    private Waypoint _start;
    private Waypoint _target;
    private Waypoint[] _waypoints;

    public override UnityEvent<State> StateChanged { get; } = new UnityEvent<State>();

    private void Start()
    {
        _waypoints = _path.GetComponentsInChildren<Waypoint>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _start = _waypoints[_currentWaypointIndex];
        _target = GetNextWaypoint();
        _rigidbody.MovePosition(_start.transform.position);
    }

    private void FixedUpdate()
    {
        if (_state == State.Running)
            MoveToWaypoint(_start, _target);
        else
            Wait();
    }

    private void MoveToWaypoint(Waypoint start, Waypoint target)
    {
        _travelRunningDuration += Time.deltaTime;
        var position = Vector2.Lerp(start.transform.position, target.transform.position, _travelRunningDuration / _travelDuration);
        _rigidbody.MovePosition(position);

        if (_travelRunningDuration >= _travelDuration)
        {
            _travelRunningDuration = 0f;
            SetState(State.Idle);
            _start = _target;
            _target = GetNextWaypoint();
        }
    }

    private void Wait()
    {
        _idleRunningDuration += Time.deltaTime;
        
        if (_idleRunningDuration >= _idleDuration)
        {
            _idleRunningDuration = 0f;
            SetState(State.Running);
        }
    }

    private void SetState(State state)
    {
        _state = state;
        StateChanged.Invoke(state);
    }

    private Waypoint GetNextWaypoint()
    {
        _currentWaypointIndex++;

        if (_currentWaypointIndex >= _waypoints.Length)
            _currentWaypointIndex = 0;

        return _waypoints[_currentWaypointIndex];
    }
}
