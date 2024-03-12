using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] private float detectionRange = 5f;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float attackDamage = 2f;
    [SerializeField] private Player _player;

    private bool isPlayerDetected = false;

    private void Update()
    {
        if (_player != null)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, _player.CurrentPosition.position);

            if (distanceToPlayer <= detectionRange)
            {
                isPlayerDetected = true;
            }

            //if (distanceToPlayer <= attackRange)
            //{
            //    AttackPlayer();
            //}

            if (isPlayerDetected)
            {
                transform.position = Vector2.MoveTowards(transform.position, _player.CurrentPosition.position, moveSpeed * Time.deltaTime);
            }
        }
    }

    //private void AttackPlayer()
    //{
    //    _player.
    //}
}
