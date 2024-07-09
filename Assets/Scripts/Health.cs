using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private int _value;

    private int _minValue = 0;
    private int _maxValue;

    public event UnityAction<int, int> Changed;
    public event UnityAction Died;

    public int CurrentHealth { get; private set; }

    private void Start()
    {
        _maxValue = _value;
        CurrentHealth = _maxValue;
        Changed?.Invoke(CurrentHealth, _value);
    }

    public void TakeDamage(int damage)
    {
        if (damage < 0)
            return;

        CurrentHealth -= damage;
        CurrentHealth = Mathf.Clamp(CurrentHealth, _minValue, _maxValue);

        if (CurrentHealth <= 0)
        {
            Died?.Invoke();
        }

        Changed?.Invoke(CurrentHealth, _value);
    }

    public void TakeHeal(int heal)
    {
        if (heal < 0)
            return;

        CurrentHealth += heal;
        CurrentHealth = Mathf.Clamp(CurrentHealth, _minValue, _maxValue);

        Changed?.Invoke(CurrentHealth, _value);
    }
}
