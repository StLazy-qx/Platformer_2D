using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private int _health;

    private int _minHealth = 0;
    private int _maxHealth;

    public int CurrentHealth { get; private set; }

    public event UnityAction<int, int> HealthChanged;
    public event UnityAction OnDeath;

    private void Start()
    {
        _maxHealth = _health;
        CurrentHealth = _maxHealth;
        HealthChanged?.Invoke(CurrentHealth, _health);
    }

    public void TakeDamage(int damage)
    {
        if (damage < 0)
            return;

        CurrentHealth -= damage;
        CurrentHealth = Mathf.Clamp(CurrentHealth, _minHealth, _maxHealth);

        if (CurrentHealth <= 0)
        {
            OnDeath?.Invoke();
        }

        HealthChanged?.Invoke(CurrentHealth, _health);
    }

    public void TakeHeal(int heal)
    {
        if (heal < 0)
            return;

        CurrentHealth += heal;
        CurrentHealth = Mathf.Clamp(CurrentHealth, _minHealth, _maxHealth);

        HealthChanged?.Invoke(CurrentHealth, _health);
    }
}
