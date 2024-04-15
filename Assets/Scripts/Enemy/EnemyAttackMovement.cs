using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(EnemyMovement))]

public class EnemyAttackMovement : MonoBehaviour 
{
    private readonly int AnimationAttack = Animator.StringToHash("Attack");

    [SerializeField] private float _distanceFindPlayer = 3f;
    [SerializeField] private float _distanceAttack = 1.2f;
    [SerializeField] private Transform _checkPoint;
    [SerializeField] private int _attackDamage = 1;

    private Player _player;
    private float _lastTimeAttack = 0;
    private float _delay = 2;
    private Animator _animator;
    private EnemyMovement _mover;

    private void Start()
    {
        _mover = GetComponent<EnemyMovement>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (CanFindPlayer())
        {
            ChaseTarget();
        }
    }

    private bool CanFindPlayer()
    {

        Vector2 direction = transform.rotation.y < 0 ? Vector2.right : Vector2.left;
        RaycastHit2D[] hits = Physics2D.RaycastAll(_checkPoint.position, direction, _distanceFindPlayer);

        foreach (var hit in hits)
        {
            if (hit.collider.TryGetComponent(out Player player))
            {
                if (player != null)
                {
                    _player = player;
                    _mover.SetPatrolling(false);

                    return true;
                }
            }
        }

        _mover.SetPatrolling(true);
        return false;
    }

    private void ChaseTarget()
    {
        float distanceBetweenTarget = Vector2.Distance(transform.position, _player.transform.position);

        if (distanceBetweenTarget <= _distanceAttack)
        {
            _mover.StopMoving();
            Attack();
        }
        else
        {
            _mover.TowardToTarget(_player.transform);
        }
    }

    private void Attack()
    {
        if (_lastTimeAttack <= 0)
        {
            _animator.SetTrigger(AnimationAttack);
            _lastTimeAttack = _delay;

            Vector2 attackDirection = transform.rotation.y < 0 ? Vector2.right : Vector2.left;
            RaycastHit2D[] hits = Physics2D.RaycastAll(_checkPoint.position, attackDirection, _distanceAttack);

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider.TryGetComponent(out Player player))
                {
                    if (player != null)
                    {
                        PersonHealthSystem healthBehaviour = player.GetComponent<PersonHealthSystem>();

                        healthBehaviour.TakeDamage(_attackDamage);
                    }
                }
            }
        }

        _lastTimeAttack -= Time.deltaTime;
    }
}
