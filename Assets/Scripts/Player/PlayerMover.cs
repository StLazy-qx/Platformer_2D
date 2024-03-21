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

    public int Damage { get; private set; }

    private void Start()
    {

        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _groundLayer = LayerMask.GetMask(Ground);
        Damage = _damage;
    }

    private void Update()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundCheckPoint.position, _groundCheckRadius, _groundLayer);
        float moveInput = Input.GetAxis(Horizontal);

        if (_isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                Jump();

            Move(moveInput);
            Flip(moveInput);
            Attack();
        }
    }

    private void Jump()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.y, _jumpForce);
    }

    private void Move(float moveInput)
    {
        _rigidbody.velocity = new Vector2(moveInput * _speedMove, _rigidbody.velocity.y);

        _animator.SetFloat(AnimationMove, Mathf.Abs(moveInput));
    }

    private void Flip(float moveInput)
    {
        transform.localScale = moveInput > 0 ? new Vector2(-1f, 1f) : Vector2.one;
    }

    private void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _animator.SetTrigger(AnimationAttack);
        }
    }
}
