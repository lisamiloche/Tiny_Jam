using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class CharacterController : MonoBehaviour
{
    [Header("Mouvements")]
    [SerializeField] float _walkSpeed;
    [SerializeField] float _acceleration;

    [Header("GroundCheck")]
    [SerializeField] float _groundOffset;
    [SerializeField] float _groundRadius;
    [SerializeField] LayerMask _groundLayer;
    bool _isGrounded;

    [Header("Jump")]
    [SerializeField] float _jumpForce;
    [SerializeField] float _timeMinBetweenJump;
    [SerializeField] float _velocityFallMin;

    [Header("Slope")]
    [SerializeField] float _slopeDetectOffset;

    Vector2 _inputs;
    bool _inputJump;
    Rigidbody2D _rb;
    float _timerNoJump;
    bool _isOnSlope;


    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        HandleInputs();
    }

    private void FixedUpdate()
    {
        HandleMovements();
        HandleGrounded();
        HandleJump();
    }

    void HandleInputs()
    {
        _inputs.x = Input.GetAxisRaw("Horizontal");
        _inputs.y = Input.GetAxisRaw("Vertical");

        _inputJump = Input.GetKey(KeyCode.UpArrow);
    }

    void HandleMovements()
    {
        var velocity = _rb.velocity;
        Vector2 wantedvelocity = new Vector2(_inputs.x * _walkSpeed, velocity.y);
        _rb.velocity = Vector2.MoveTowards(velocity, wantedvelocity, _acceleration);
    }

    void HandleGrounded()
    {
        Vector2 point = transform.position + Vector3.up * _groundOffset;
        Collider2D[] _collidersGround = new Collider2D[2];

        bool currentGrounded =
            Physics2D.OverlapCircleNonAlloc(point, _groundRadius, _collidersGround, _groundLayer) > 0; 
        _isGrounded = currentGrounded;
    }

    void HandleJump()
    {
        _timerNoJump -= Time.deltaTime;

        if (_inputJump && _rb.velocity.y <= 0 && _isGrounded && _timerNoJump <= 0)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
            _timerNoJump = _timeMinBetweenJump;
        }

        if (_rb.velocity.y > _velocityFallMin)
        { 
                _rb.velocity = new Vector2(_rb.velocity.x, _velocityFallMin);
        }
    }

   /* void HandleSlope()
    {
        Vector3 origin = transform.position + Vector3.up * _groundOffset;
        RaycastHit2D[] _hitResults = new RaycastHit2D[2];

        bool slopeRight =
            Physics2D.RaycastNonAlloc(origin, Vector2.right, _hitResults, _slopeDetectOffset, _groundLayer) > 0;

        bool slopeLeft = 
            Physics2D.RaycastNonAlloc(origin, Vector2.left, _hitResults, _slopeDetectOffset, _groundLayer) > 0;

        _isOnSlope = (slopeRight || slopeLeft) && (slopeRight == false || slopeLeft == false);

        if (Mathf.Abs(_inputs.x) < 00.1f && (slopeLeft || slopeRight))
        {
        }
    }*/
}
