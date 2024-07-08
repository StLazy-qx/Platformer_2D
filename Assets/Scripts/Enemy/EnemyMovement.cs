using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform[] _movePoints;
    [SerializeField] private Health _health;
    [SerializeField] private float _speedMove;
    [SerializeField] private float _waitTime = 1.5f;

    private Animator _animator;
    private Coroutine _patrolCoroutine;
    private readonly int _animationMove = Animator.StringToHash("isMoving");
    private readonly int _animationDie = Animator.StringToHash("isDie");
    private int _pointIndex = 0;
    private bool _isMoving = false;
    private bool _isPatrolling = true;
    private bool _isDead = false;




    private void Start()
    {
        _animator = GetComponent<Animator>();
        _health.Died += HandleDeath;

        StartPatrol();
    }

    private void OnDestroy()
    {
        _health.Died -= HandleDeath;
    }

    private void HandleDeath()
    {
        _isDead = true;

        StopPatrol();

        _animator.SetBool(_animationMove, false);
        _animator.SetTrigger(_animationDie);
        enabled = false;
    }

    public void SetPatrolling(bool isPatrolling)
    {
        if (_isDead)
            return;

        _isPatrolling = isPatrolling;
        if (isPatrolling)
            StartPatrol();
        else
            StopPatrol();
    }

    public void StopMoving()
    {
        _speedMove = 0;
        _animator.SetBool(_animationMove, false);
    }

    public void TowardToTarget(Transform target)
    {
        if (_isDead)
            return;

        _speedMove = 2;
        _animator.SetBool(_animationMove, true);
        transform.position = Vector2.MoveTowards(transform.position,
            target.transform.position, _speedMove * Time.deltaTime);
    }

    private void StartPatrol()
    {
        if (_isDead)
            return;

        if (_patrolCoroutine == null)
            _patrolCoroutine = StartCoroutine(FollowPath());
    }

    private void StopPatrol()
    {
        if (_patrolCoroutine != null)
        {
            StopCoroutine(_patrolCoroutine);
            _patrolCoroutine = null;
        }
    }

    private IEnumerator FollowPath()
    {
        _speedMove = 2;

        while (_isPatrolling)
        {
            Vector2 targetPosition = _movePoints[_pointIndex].position;
            WaitForSeconds waitForSeconds = new WaitForSeconds(_waitTime);

            RotateTowardTarget(_movePoints[_pointIndex]);

            _isMoving = true;
            _animator.SetBool(_animationMove, _isMoving);

            while ((Vector2)transform.position != targetPosition)
            {
                transform.position = Vector2.MoveTowards(transform.position, 
                    targetPosition, _speedMove * Time.deltaTime);

                yield return null;
            }

            _isMoving = false;
            _animator.SetBool(_animationMove, _isMoving);

            yield return waitForSeconds;

            _pointIndex = ++_pointIndex % _movePoints.Length;
        }
    }

    private void RotateTowardTarget(Transform target)
    {
        if (_isDead)
            return;

        Vector2 enemyPosition = transform.position;
        Vector2 targetPosition = target.position;
        transform.rotation = targetPosition.x < enemyPosition.x ? Quaternion.Euler(0f, 0f, 0f) : Quaternion.Euler(0f, 180f, 0f);
    }
}
