using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]

public class PlayerMover : MonoBehaviour
{
    private readonly string Ground = "Ground";
    private readonly string Horizontal = "Horizontal";
    private readonly int AnimationMove = Animator.StringToHash("Move");
    private readonly int AnimationAttack = Animator.StringToHash("Attack");

    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private float _speedMove = 7f;
    [SerializeField] private Transform _groundCheckPoint;
    [SerializeField] private int _damage = 2;

    private float _groundCheckRadius = 0.2f;
    private bool _isGrounded;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private LayerMask _groundLayer;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _groundLayer = LayerMask.GetMask(Ground);
    }

    private void Update()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundCheckPoint.position, _groundCheckRadius, _groundLayer);
        float moveInput = Input.GetAxis(Horizontal);
        float moveDirection = moveInput != 0 ? Mathf.Sign(moveInput) : 0;

        if (_isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                Jump();

            Move(moveInput);
            Attack();
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

        _animator.SetFloat(AnimationMove, Mathf.Abs(value));
    }

    private void Flip(float value)
    {
        if (value != 0)
        {
            transform.rotation = value > 0 ? Quaternion.Euler(0f, 180f, 0f) : Quaternion.Euler(0f, 0f, 0f);
        }
    }

    private void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _animator.SetTrigger(AnimationAttack);
        }
    }
}
