using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMover : MonoBehaviour
{
    private readonly string Ground = "Ground";
    private readonly string Horizontal = "Horizontal";
    private readonly int AnimationMove = Animator.StringToHash("Move");
    private readonly int AnimationAttack = Animator.StringToHash("Attack");

    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private float _speedMove = 7f;
    [SerializeField] private Transform _groundCheckPoint;
    [SerializeField] private Animator _animator;
    [SerializeField] private int _damage = 2;

    private float _groundCheckRadius = 0.2f;
    private bool _isGrounded;
    private Rigidbody2D _rigidbody;
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
        if (moveInput > 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (moveInput < 0)
        {
            transform.localScale = Vector3.one;
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
