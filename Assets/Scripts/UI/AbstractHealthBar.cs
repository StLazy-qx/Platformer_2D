using UnityEngine;

public abstract class AbstractHealthBar : MonoBehaviour
{
    [SerializeField] private Health _health;

    private void OnEnable()
    {
        _health.Changed += OnHealthChanged;
    }

    private void OnDisable()
    {
        _health.Changed -= OnHealthChanged;
    }

    protected virtual void OnHealthChanged (int value, int maxValue) { }
}
