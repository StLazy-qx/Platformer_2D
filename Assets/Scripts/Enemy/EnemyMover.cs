using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyPatrolMover))]

public class EnemyMover : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _moveSpeed = 3f;
    [SerializeField] private float _detectionDistance = 5f;

    private EnemyPatrolMover _enemyPatrol;
    private Vector2 _originalPosition;
    private bool _isChasing = false;

    private void Start()
    {
        _originalPosition = transform.position;
        _enemyPatrol = GetComponent<EnemyPatrolMover>();
    }

    private void Update()
    {
        float distanceToTarget = Vector2.Distance(transform.position, _target.position);

        if (distanceToTarget < _detectionDistance)
        {
            _isChasing = true;
        }


        if (_isChasing)
        {
            transform.position = Vector2.MoveTowards(transform.position, _target.position, _moveSpeed * Time.deltaTime);


        }
    }
}
