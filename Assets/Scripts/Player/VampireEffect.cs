using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Health))]

public class VampireEffect : MonoBehaviour
{
    [SerializeField] private Transform _vampireCheckPoint;

    private Health _playerHealth;
    private Coroutine _vampireCoroutine;
    private KeyCode _vampireKey = KeyCode.Q;
    private float _radius = 3f;
    private float _timeAction = 6f;
    private float _damagePerSecond = 5f;
    private bool _isVampirismReady = true;

    public event UnityAction UsingVampirism;
    public event UnityAction Cooldowning;

    private void Start()
    {
        _playerHealth = GetComponent<Health>();
    }

    private void Update()
    {
        if (_isVampirismReady)
        {
            if (Input.GetKeyDown(_vampireKey))
            {
                UsingVampirism?.Invoke();
                _isVampirismReady = false;

                DefineEnemy();
            }
        }
    }

    public void SetVampirismReady(bool isReady)
    {
        _isVampirismReady = isReady;
    }

    private void DefineEnemy()
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

        if (nearestEnemy != null)
        {
            if (_vampireCoroutine != null)
            {
                StopCoroutine(_vampireCoroutine);
            }

            _vampireCoroutine = StartCoroutine(PerformVampirism(nearestEnemy));
        }
    }

    private IEnumerator PerformVampirism(Enemy enemy)
    {
        float elapsedTime = 0f;
        float healthAccumulated = 0f;

        while (elapsedTime < _timeAction)
        {
            elapsedTime += Time.deltaTime;
            healthAccumulated += _damagePerSecond * Time.deltaTime;

            if (enemy != null)
            {
                Health enemyHealth = enemy.GetComponent<Health>();

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
}
