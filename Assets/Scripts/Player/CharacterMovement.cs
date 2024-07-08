using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private Transform _groundCheckPoint;
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private float _speedMove = 7f;

    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private LayerMask _groundLayer;
    private float _groundCheckRadius = 0.2f;
    private readonly int _animationMove = Animator.StringToHash("Move");
    private readonly string _ground = "Ground";
    private readonly string _horizontal = "Horizontal";
    private KeyCode _jumpKey = KeyCode.Space;
    private bool _isGrounded;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _groundLayer = LayerMask.GetMask(_ground);
    }

    private void Update()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundCheckPoint.position, _groundCheckRadius, _groundLayer);
        float moveInput = Input.GetAxis(_horizontal);
        float moveDirection = moveInput != 0 ? Mathf.Sign(moveInput) : 0;

        if (_isGrounded)
        {
            if (Input.GetKeyDown(_jumpKey))
                Jump();

            Move(moveInput);
            Flip(moveDirection);
        }
    }

    private void Jump()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.y, _jumpForce);
    }

    private void Move(float value)
    {
        _rigidbody.velocity = new Vector2(value * _speedMove, _rigidbody.velocity.y);

        _animator.SetFloat(_animationMove, Mathf.Abs(value));
    }

    private void Flip(float value)
    {
        if (value != 0)
        {
            transform.rotation = value > 0 ? Quaternion.Euler(0f, 180f, 0f) : Quaternion.Euler(0f, 0f, 0f);
        }
    }
}
