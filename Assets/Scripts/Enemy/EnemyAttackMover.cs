using UnityEngine;

[RequireComponent(typeof(Animator))]

public class EnemyAttackMover : MonoBehaviour 
{
    [SerializeField] private int _attackDamage = 30;

    private Animator _animator;
    private float _lastTimeAttack = 0;
    private float _delay = 2;
    private readonly int _animationAttack = Animator.StringToHash("Attack");

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void PerformAttack(RaycastHit2D[] hits)
    {
        if (_lastTimeAttack <= 0)
        {
            _lastTimeAttack = _delay;

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.collider.TryGetComponent(out Player player))
                {
                    if (player != null)
                    {
                        _animator.SetTrigger(_animationAttack);

                        Health healthBehaviour = player.GetComponent<Health>();
                        healthBehaviour.TakeDamage(_attackDamage);
                    }
                }
            }
        }

        _lastTimeAttack -= Time.deltaTime;
    }
}