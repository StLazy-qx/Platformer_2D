using UnityEngine;

[RequireComponent(typeof(Animator))]

public class EnemyAttackMover : MonoBehaviour 
{
    [SerializeField] private int _attackDamage = 15;

    private Animator _animator;
    private float _lastTimeAttack = 0;
    private float _delay = 2;
    private readonly int _animationAttack = Animator.StringToHash("Attack");
    private RaycastHit2D[] _hits;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void PerformAttack(RaycastHit2D[] hits)
    {
        if (_lastTimeAttack <= 0)
        {
            _hits = hits;
            _animator.SetTrigger(_animationAttack);
            _lastTimeAttack = _delay;
        }

        _lastTimeAttack -= Time.deltaTime;
    }

    private void DealDamage()
    {
        foreach (RaycastHit2D hit in _hits)
        {
            if (hit.collider.TryGetComponent(out Player player))
            {
                if (player != null)
                {
                    Health healthBehaviour = player.GetComponent<Health>();
                    healthBehaviour.TakeDamage(_attackDamage);
                }
            }
        }
    }
}