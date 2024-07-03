using UnityEngine;

public abstract class HealthbarView : MonoBehaviour
{
    [SerializeField] protected Health Health;

    private void OnEnable()
    {
        Health.HealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        Health.HealthChanged -= OnHealthChanged;
    }

    protected virtual void OnHealthChanged(int value, int maxValue) { }
}
