using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SmoothHealthBar : HealthbarView
{
    [SerializeField] private Slider _slider;

    private Coroutine _smoothChangeValue;

    private void Start()
    {
        _slider.value = 1;
    }

    protected override void OnHealthChanged(int value, int maxValue)
    {
        SetSliderValue(value, maxValue);
    }

    private void SetSliderValue(int value, int maxValue)
    {
        if (_smoothChangeValue != null)
        {
            StopCoroutine(_smoothChangeValue);
        }

        _smoothChangeValue = StartCoroutine(ChangeSliderValue(value, maxValue));
    }

    private IEnumerator ChangeSliderValue(float value, int maxValue)
    {
        float durationSmoth = 1.0f;
        float elapsedTime = 0f;
        float currentValue = (float)value / maxValue;
        float distance = Mathf.Abs(currentValue - _slider.value);

        while (elapsedTime < durationSmoth)
        {
            elapsedTime += Time.deltaTime;
            _slider.value = Mathf.MoveTowards(_slider.value, currentValue, distance * Time.deltaTime);

            yield return null;
        }

        _slider.value = currentValue;
        _smoothChangeValue = null;
    }
}
