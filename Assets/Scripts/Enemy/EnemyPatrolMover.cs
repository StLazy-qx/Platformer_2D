using System.Collections;
using UnityEngine;

public class EnemyPatrolMover : MonoBehaviour
{
    [SerializeField] private Transform[] _movePoints;
    [SerializeField] private float _speedMove = 2f;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _waitTime = 1.5f;

    private bool _isMoving = false;
    private int _pointIndex = 0;

    private void Start()
    {
        _animator = GetComponent<Animator>();

        StartCoroutine(MoveToNextPoint());
    }

    private IEnumerator MoveToNextPoint()
    {
        while (true)
        {
            Vector2 targetPosition = _movePoints[_pointIndex].position;

            RotateTowardTarget(_movePoints[_pointIndex]);

            _isMoving = true;
            _animator.SetBool("isMoving", _isMoving);

            while ((Vector2)transform.position != targetPosition)
            {
                transform.position = Vector2.MoveTowards(transform.position, 
                    targetPosition, _speedMove * Time.deltaTime);

                yield return null;
            }

            _isMoving = false;
            _animator.SetBool("isMoving", _isMoving);

            yield return new WaitForSeconds(_waitTime);

            _pointIndex = (_pointIndex + 1) % _movePoints.Length;
        }
    }

    private void RotateTowardTarget(Transform target)
    {
        Vector3 direction = (transform.position - target.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0f, angle, 0f);
        transform.rotation = rotation;
    }
}
