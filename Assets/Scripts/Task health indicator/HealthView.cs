using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour
{
    [SerializeField] private PersonHealthSystem _healthPlayer;
    [SerializeField] private Slider _smothSlider;
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _healthText;

    private Coroutine _smothChangeValue;

    private void OnEnable()
    {
        _healthPlayer.HealthChanged += OnHealthChanged;
        _slider.value = 1;
        _smothSlider.value = 1;
    }

    private void OnDisable()
    {
        _healthPlayer.HealthChanged -= OnHealthChanged;
    }

    private void OnHealthChanged(int value, int maxValue)
    {
        _slider.value = (float)value / maxValue;
        _healthText.text = $"{value} / {maxValue}";

        SetSliderValue(value, maxValue);
    }

    private void SetSliderValue(int value, int maxValue)
    {
        if (_smothChangeValue != null)
        {
            StopCoroutine(_smothChangeValue);
        }

        _smothChangeValue = StartCoroutine(ChangeSliderValue(value, maxValue));
    }

    private IEnumerator ChangeSliderValue(float value, int maxValue)
    {
        float durationSmoth = 1.0f;
        float elapsedTime = 0f;
        float currentValue = (float)value / maxValue;
        float distance = Mathf.Abs(currentValue - _smothSlider.value);

        while (elapsedTime < durationSmoth)
        {
            elapsedTime += Time.deltaTime;
            _smothSlider.value = Mathf.MoveTowards(_smothSlider.value, currentValue, distance  * Time.deltaTime);

            yield return null;
        }

        _smothSlider.value = currentValue;
        _smothChangeValue = null;
    }
}
