using UnityEngine;
using TMPro;

public class TextHealthbar : HealthbarView
{
    [SerializeField] private TextMeshProUGUI _healthText;

    protected override void OnHealthChanged(int value, int maxValue)
    {
        _healthText.text = $"{value} / {maxValue}";
    }
}
