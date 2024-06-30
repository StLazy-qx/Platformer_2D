using UnityEngine;

public abstract class HealthbarView : MonoBehaviour
{
    [SerializeField] protected PersonHealthSystem HealthPlayer;

    private void OnEnable()
    {
        HealthPlayer.HealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        HealthPlayer.HealthChanged -= OnHealthChanged;
    }

    protected virtual void OnHealthChanged(int value, int maxValue) { }
}
