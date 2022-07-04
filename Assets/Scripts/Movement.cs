using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Collider2D))]
public class Movement : MonoBehaviour
{
    [SerializeField] private float _horizontalSpeed = 0.1f;
    [SerializeField] private float _jumpForce = 0.1f;
    [SerializeField] LayerMask _groundLayer;
    [SerializeField] private float _minGroundDistance = 0.1f;

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private Collider2D _collider;
    private bool _isGrounded = true;
    private Vector2 _inputVector = Vector2.zero;

    private Vector2 _groundBoxCastSize;
    private Vector2 _physicCenterDelta;

    private Vector2 ColliderCenter => (Vector2)transform.position + _physicCenterDelta;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _collider = GetComponent<Collider2D>();
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
        MovePosition();
    }

    private void MovePosition()
    {
        if (_inputVector == Vector2.zero)
            return;

        _rigidbody.velocity = new Vector2(_inputVector.x * _horizontalSpeed, _rigidbody.velocity.y);

        if (_inputVector.y == 1f && CheckGround())
        {
            _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            _inputVector.y = 0f;
        }
    }

    private bool CheckGround()
    {
        return Physics2D.BoxCast(ColliderCenter, _groundBoxCastSize, 0f, Vector2.down, _minGroundDistance, _groundLayer);
    }
}
