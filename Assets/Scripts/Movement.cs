using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Movement : AnimationEventSource
{
    [SerializeField] private float _horizontalSpeed = 0.1f;
    [SerializeField] private float _jumpForce = 0.1f;
    [SerializeField] private float _minGroundDistance = 0.1f;
    [SerializeField] private LayerMask _groundLayer;

    private Collider2D _collider;
    private Rigidbody2D _rigidbody;
    private Vector2 _groundBoxCastSize;
    private Vector2 _inputVector = Vector2.zero;
    private Vector2 _physicCenterDelta;
    private State _state = State.Idle;

    private Vector2 ColliderCenter => (Vector2)transform.position + _physicCenterDelta;

    public override UnityEvent<State> StateChanged { get; } = new UnityEvent<State>();


    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _groundBoxCastSize = new Vector2(_collider.bounds.size.x, _collider.bounds.size.y / 2 + _minGroundDistance);
        _physicCenterDelta = transform.position - _collider.bounds.center;
    }

    private void Update()
    {
        _inputVector = new Vector2(Input.GetAxis("Horizontal"), 0f);

        if (Input.GetKey(KeyCode.Space))
            _inputVector.y = 1f;
    }

    private void FixedUpdate()
    {
        var state = State.Idle;

        if (_inputVector != Vector2.zero)
        {
            _rigidbody.velocity = new Vector2(_inputVector.x * _horizontalSpeed, _rigidbody.velocity.y);
            state = State.Running;

            if (_inputVector.y == 1f && CheckGround())
            {
                _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
                _inputVector.y = 0f;
                state = State.Jumping;
            }
        }

        SetState(state);
    }

    private bool CheckGround()
    {
        return Physics2D.BoxCast(ColliderCenter, _groundBoxCastSize, 0f, Vector2.down, _minGroundDistance, _groundLayer);
    }

    private void SetState(State state)
    {
        if (state == _state) return;

        _state = state;
        StateChanged.Invoke(state);
    }
}
