using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private float _speedMove = 7f;
    [SerializeField] private Transform _groundCheckPoint;
    [SerializeField] private Animator _animator;
    [SerializeField] private int _damage = 2;

    private float _groundCheckRadius = 0.2f;
    private bool isGrounded;
    private Rigidbody2D _rigidbody;
    private LayerMask _groundLayer;

    public int Damage { get; private set; }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _groundLayer = LayerMask.GetMask("Ground");
        Damage = _damage;
    }

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(_groundCheckPoint.position, _groundCheckRadius, _groundLayer);
        float moveInput = Input.GetAxis("Horizontal");

        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space))
                Jump();

            Attack();
            Move(moveInput);
            Flip(moveInput);
        }
    }

    private void Jump()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.y, _jumpForce);
    }

    private void Move(float moveInput)
    {
        _rigidbody.velocity = new Vector2(moveInput * _speedMove, _rigidbody.velocity.y);

        _animator.SetFloat("Move", Mathf.Abs(moveInput));
    }

    private void Flip(float moveInput)
    {
        if (moveInput > 0)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (moveInput < 0)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    private void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _animator.SetTrigger("Attack");
        }
    }
}
