using UnityEngine;

[RequireComponent(typeof(EnemyPatrolMover))]
[RequireComponent(typeof(EnemyAttackMover))]

public class EnemyChaseMover : MonoBehaviour
{
    [SerializeField] private Transform _checkPoint;
    [SerializeField] private float _distanceFindPlayer = 3f;
    [SerializeField] private float _distanceAttack = 1.5f;

    private Player _player;
    private EnemyPatrolMover _patrolMover;
    private EnemyAttackMover _attackMover;
    private RaycastHit2D[] _hits;

    private void Start()
    {
        _patrolMover = GetComponent<EnemyPatrolMover>();
        _attackMover = GetComponent<EnemyAttackMover>();
    }

    private void Update()
    {
        if (_patrolMover.IsDead)
        {
            enabled = false;

            return;
        }

        if (CanFindPlayer())
        {
            ChaseTarget();
        }
    }

    private bool CanFindPlayer()
    {
        Vector2 direction = transform.rotation.y < 0 ? Vector2.right : Vector2.left;
        _hits = Physics2D.RaycastAll(_checkPoint.position, direction, _distanceFindPlayer);

        foreach (var hit in _hits)
        {
            if (hit.collider.TryGetComponent(out Player player))
            {
                if (player != null)
                {
                    _player = player;
                    _patrolMover.SetPatrolling(false);

                    return true;
                }
            }
        }

        _patrolMover.SetPatrolling(true);
        return false;
    }

    private void ChaseTarget()
    {
        float distanceBetweenTarget = Vector2.Distance(transform.position, _player.transform.position);

        if (distanceBetweenTarget <= _distanceAttack)
        {
            _patrolMover.StopMoving();
            _attackMover.PerformAttack(_hits);
        }
        else
        {
            _patrolMover.TowardToTarget(_player.transform);
        }
    }
}
