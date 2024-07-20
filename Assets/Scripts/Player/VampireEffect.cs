using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Health))]

public class VampireEffect : MonoBehaviour
{
    [SerializeField] private Transform _vampireCheckPoint;
    [SerializeField] private float _timeAction = 6f;

    private Health _playerHealth;
    private Coroutine _vampireCoroutine;
    private KeyCode _vampireKey = KeyCode.Q;
    private float _radius = 3f;
    private float _damagePerSecond = 5f;
    private bool _isVampirismReady = true;

    public event UnityAction UsingVampirism;
    public event UnityAction Cooldowning;

    public float TimeAction => _timeAction;

    private void Start()
    {
        _playerHealth = GetComponent<Health>();
    }

    private void Update()
    {
        if (_isVampirismReady && Input.GetKeyDown(_vampireKey))
        {
            _isVampirismReady = false;

            UsingVampirism?.Invoke();
            IncludeVampirism();
        }
    }

    public void EnableVampirism()
    {
        _isVampirismReady = true;
    }

    private void IncludeVampirism()
    {
        if (_vampireCoroutine != null)
        {
            StopCoroutine(_vampireCoroutine);
        }

        _vampireCoroutine = StartCoroutine(PerformVampirism());
    }

    private IEnumerator PerformVampirism()
    {
        float healthAccumulated = 0f;
        int iterations = Mathf.CeilToInt(_timeAction / Time.deltaTime);

        for (int i = 0; i < iterations; i++)
        {
            healthAccumulated += _damagePerSecond * Time.deltaTime;

            Enemy nearestEnemy = GetNearestEnemy();

            if (nearestEnemy != null)
            {
                Health enemyHealth = nearestEnemy.GetComponent<Health>();
                if (healthAccumulated >= 1f)
                {
                    int healthToTransfer = Mathf.FloorToInt(healthAccumulated);
                    healthAccumulated -= healthToTransfer;

                    enemyHealth.TakeDamage(healthToTransfer);
                    _playerHealth.TakeHeal(healthToTransfer);
                }
            }

            yield return null;
        }

        Cooldowning?.Invoke();

        _vampireCoroutine = null;
    }

    private Enemy GetNearestEnemy()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(_vampireCheckPoint.position, _radius);
        Enemy nearestEnemy = null;
        float nearestDistance = Mathf.Infinity;

        foreach (Collider2D hit in hits)
        {
            if (hit.TryGetComponent(out Enemy enemy))
            {
                float distance = Vector2.Distance(_vampireCheckPoint.position, enemy.transform.position);

                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    nearestEnemy = enemy;
                }
            }
        }

        return nearestEnemy;
    }
}
