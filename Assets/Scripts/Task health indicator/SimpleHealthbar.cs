using UnityEngine;
using UnityEngine.UI;

public class SimpleHealthbar : HealthbarView
{
    [SerializeField] private Slider _slider;

    private void Start()
    {
        _slider.value = 1;
    }

    protected override void OnHealthChanged(int value, int maxValue)
    {
        _slider.value = (float)value / maxValue;
    }
}
