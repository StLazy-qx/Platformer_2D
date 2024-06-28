using UnityEngine;
using UnityEngine.Events;

public class PersonHealthSystem : MonoBehaviour
{
    [SerializeField] private int _health;

    public int CurrentHealth { get; private set; }

    public event UnityAction<int, int> HealthChanged;

    private void Start()
    {
        CurrentHealth = _health;
        HealthChanged?.Invoke(CurrentHealth, _health);
    }

    public void TakeDamage(int damage)
    {
        CurrentHealth -= damage;

        if (CurrentHealth <= 0)
            CurrentHealth = 0;

        HealthChanged?.Invoke(CurrentHealth, _health);
    }

    public void TakeHeal(int heal)
    {
        CurrentHealth += heal;

        if (CurrentHealth >= _health)
            CurrentHealth = _health;

        HealthChanged?.Invoke(CurrentHealth, _health);
    }
}
