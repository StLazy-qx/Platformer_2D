using UnityEngine;

[RequireComponent(typeof(Animator))]

public class PlayerAttackMovement : MonoBehaviour
{
    private readonly int AnimationAttack = Animator.StringToHash("Attack");

    [SerializeField] private Transform _attackPoint;

    private Animator _animator;
    private bool _isAttacking = false;
    private float _attackRange = 1.5f;
    private int _attackDamage = 50;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !_isAttacking)
        {
            PerformAttack();
            _animator.SetTrigger(AnimationAttack);
        }
    }

    private void PerformAttack()
    {
        _isAttacking = true;
        float timeBetweenAttacking = 1f;

        Vector2 attackDirection = transform.rotation.y < 0 ? Vector2.right : Vector2.left;
        RaycastHit2D[] hits = Physics2D.RaycastAll(_attackPoint.position, attackDirection, _attackRange);

        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.TryGetComponent(out Enemy enemy))
            {
                if (enemy != null)
                {
                    Health healthBehaviour = enemy.GetComponent<Health>();

                    healthBehaviour.TakeDamage(_attackDamage);
                }
            }
        }

        Invoke("ResetAttack", timeBetweenAttacking);
    }

    private void ResetAttack()
    {
        _isAttacking = false;
    }
}
