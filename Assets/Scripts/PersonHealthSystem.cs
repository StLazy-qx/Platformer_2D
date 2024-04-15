using UnityEngine;
using UnityEngine.Events;

public class PersonHealthSystem : MonoBehaviour
{
    [SerializeField] private int _health;

    private int _currentHealth;

    public event UnityAction<int, int> MoneyChanged;

    private void Start()
    {
        _currentHealth = _health;
        MoneyChanged?.Invoke(_currentHealth, _health);
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        MoneyChanged?.Invoke(_currentHealth, _health);
    }

    public void TakeHeal(int heal)
    {
        if (_currentHealth > _health)
            _currentHealth = _health;

        _currentHealth += heal;
        MoneyChanged?.Invoke(_currentHealth, _health);
    }
}
