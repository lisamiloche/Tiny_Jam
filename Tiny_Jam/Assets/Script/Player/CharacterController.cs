using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

public class CharacterController : MonoBehaviour
{
    [Header("Mouvements")]
    [SerializeField] float _walkSpeed;
    [SerializeField] float _acceleration;

    [Header("GroundCheck")]
    [SerializeField] float _groundOffset;
    [SerializeField] float _groundRadius;
    [SerializeField] Transform groundChecker;
    [SerializeField] LayerMask _groundLayer;
    public bool _isGrounded;

    [Header("Jump")]
    [SerializeField] float _jumpForce;
    [SerializeField] float _timeMinBetweenJump;
    [SerializeField] float _velocityFallMin;

    [Header("Slope")]
    [SerializeField] float _slopeDetectOffset;
    [SerializeField] PhysicsMaterial2D _physicsFriction;
    [SerializeField] PhysicsMaterial2D _physicsNoFriction;

    Vector2 _inputs;
    bool _inputJump;
    Rigidbody2D _rb;
    float _timerNoJump;
    bool _isOnSlope;
    Collider2D _collider;
    [SerializeField] Collider2D _playingZone;
    public bool _isLookingRight;
    Vector2 _checkDirection;
    bool _canMove;
    AnimManager _animManager;

    public bool isRunning = false;
    public bool isJumping = false;
    public bool isFalling = false;
    
    private void Awake()
    {
        _animManager = GetComponent<AnimManager>();
        AudioManager.Instance.PlayMusic(0, true);
    }

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _collider = GetComponent<Collider2D>();
    }


    private void Update()
    {
        HandleInputs();
        CheckDirection();
        PlayAnimation();
    }

    private void PlayAnimation()
    {
        _animManager.SetBool("isJumping", isJumping);
        _animManager.SetBool("isFalling", isFalling);
        _animManager.SetBool("isRunning", isRunning);

        if (isRunning)
        {
            _animManager.PlayAnimation("player_run");
            Debug.Log("Anim Run");
        }
        else if (isJumping)
        { 
            _animManager.PlayAnimation("player_jump_up");
            Debug.Log("Anim Saut up");
        }
        else if (isFalling)
        {
            _animManager.PlayAnimation("player_jump_down");
            Debug.Log("Anim Saut down");
        }
        else
        {
            _animManager.PlayAnimation("player_idle");
            Debug.Log("Anim Idle");
        }
    }

    private void FixedUpdate()
    {
        HandleMovements();
        HandleGrounded();
        HandleJump();
        HandleSlope();
        RestrictToPlayingZone();
    }

    void HandleInputs()
    {
        _inputs.x = Input.GetAxisRaw("Horizontal");
        _inputs.y = Input.GetAxisRaw("Vertical");

        _inputJump = Input.GetKey(KeyCode.UpArrow);
    }

    void HandleMovements()
    {
        if(Mathf.Abs(_inputs.x) > 0.1f)
        {
            var velocity = _rb.velocity;
            Vector2 wantedvelocity = new Vector2(_inputs.x * _walkSpeed, velocity.y);
            _rb.velocity = Vector2.MoveTowards(velocity, wantedvelocity, _acceleration);
            
            if(_isGrounded)
                isRunning = true;
        }
        else
            isRunning = false;
        
    }

    void HandleGrounded()
    {
        Collider2D[] _collidersGround = new Collider2D[2];

        bool currentGrounded =
            Physics2D.OverlapCircleNonAlloc(groundChecker.position, _groundRadius, _collidersGround, _groundLayer) > 0; 
        _isGrounded = currentGrounded;
    }


    void HandleJump()
    {
        _timerNoJump -= Time.deltaTime;

        if (_isGrounded)
        {
            isJumping = false;
            isFalling = false;

            if (_inputJump && _rb.velocity.y <= 0 && _timerNoJump <= 0)
            {
                Debug.Log("On saute");
                _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);
                _timerNoJump = _timeMinBetweenJump;
                isJumping = true;
            }
        }
        else
        {
            if (_rb.velocity.y < 0)
            {
                Debug.Log("On retombe");
                isFalling = true;
            }
            else if (_rb.velocity.y > 0)
            {
                isJumping = true;
                isFalling = false;
            }
        }

        if (_rb.velocity.y < -_velocityFallMin)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, -_velocityFallMin);
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(groundChecker.position, _groundRadius);
    }


    void HandleSlope()
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
            _collider.sharedMaterial = _physicsFriction;
        }
        else
        {
            _collider.sharedMaterial = _physicsNoFriction;
        }
    }

    void CheckDirection()
    {
        if (_inputs.x > 0)
        {
            _isLookingRight = true;
            _checkDirection.x = _inputs.x;
        }
        else if (_inputs.x < 0)
        {
            _isLookingRight = false;
            _checkDirection.x = _inputs.x;
        }
        else if (_inputs.x == 0)
        {
            if (_checkDirection.x > 0)
                _isLookingRight = true;
            else
                _isLookingRight = false;
        }
    }

    void RestrictToPlayingZone()
    {
        Vector3 position = transform.position;
        Bounds bounds = _playingZone.bounds;

        if (position.x < bounds.min.x)
        {
            position.x = bounds.min.x;
        }
        else if (position.x > bounds.max.x)
        {
            position.x = bounds.max.x;
        }

        if (position.y < bounds.min.y)
        {
            position.y = bounds.min.y;
        }

        transform.position = position;
    }

}
