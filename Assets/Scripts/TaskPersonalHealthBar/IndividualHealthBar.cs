using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class IndividualHealthBar : HealthbarView
{
    [SerializeField] private Image _healthbar;

    private Coroutine _smoothChangeValue;

    private void Start()
    {
        _healthbar.fillAmount = 1;
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
        float distance = Mathf.Abs(currentValue - _healthbar.fillAmount);

        while (elapsedTime < durationSmoth)
        {
            elapsedTime += Time.deltaTime;
            _healthbar.fillAmount = Mathf.MoveTowards(_healthbar.fillAmount, currentValue, distance * Time.deltaTime);

            yield return null;
        }

        _healthbar.fillAmount = currentValue;
        _smoothChangeValue = null;
    }
}
