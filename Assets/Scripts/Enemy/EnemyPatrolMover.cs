using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]

public class EnemyPatrolMover : MonoBehaviour
{
    private readonly int AnimationMove = Animator.StringToHash("isMoving");

    [SerializeField] private Transform[] _movePoints;
    [SerializeField] private float _speedMove = 2f;
    [SerializeField] private float _waitTime = 1.5f;

    private Animator _animator;
    private bool _isMoving = false;
    private int _pointIndex = 0;

    private void Start()
    {
        _animator = GetComponent<Animator>();

        StartCoroutine(FollowPath());
    }

    private IEnumerator FollowPath()
    {
        while (true)
        {
            Vector2 targetPosition = _movePoints[_pointIndex].position;
            WaitForSeconds waitForSeconds = new WaitForSeconds(_waitTime);

            RotateTowardTarget(_movePoints[_pointIndex]);

            _isMoving = true;
            _animator.SetBool(AnimationMove, _isMoving);

            while ((Vector2)transform.position != targetPosition)
            {
                transform.position = Vector2.MoveTowards(transform.position, 
                    targetPosition, _speedMove * Time.deltaTime);

                yield return null;
            }

            _isMoving = false;
            _animator.SetBool(AnimationMove, _isMoving);

            yield return waitForSeconds;

            _pointIndex = ++_pointIndex % _movePoints.Length;
        }
    }

    private void RotateTowardTarget(Transform target)
    {
        Vector2 enemyPosition = transform.position;
        Vector2 targetPosition = target.position;
        transform.rotation = targetPosition.x < enemyPosition.x ? Quaternion.Euler(0f, 0f, 0f) : Quaternion.Euler(0f, 180f, 0f);
    }
}
