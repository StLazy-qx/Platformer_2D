using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : AbstractHealthBar
{
    [SerializeField] private Image _healthbar;

    private Coroutine _smoothChangeValue;

    protected override void OnHealthChanged(int value, int maxValue)
    {
        SetHealthbarValue(value, maxValue);
    }

    private void SetHealthbarValue(int value, int maxValue)
    {
        if (_smoothChangeValue != null)
        {
            StopCoroutine(_smoothChangeValue);
        }

        _smoothChangeValue = StartCoroutine(ChangeSliderValue(value, maxValue));
    }

    private IEnumerator ChangeSliderValue(float value, int maxValue)
    {
        if (_healthbar == null)
            yield break;

        if (maxValue == 0)
            yield break;

        float durationSmoth = 1.0f;
        float elapsedTime = 0f;
        float startValue = _healthbar.fillAmount;
        float endValue = (float)value / maxValue;

        while (elapsedTime < durationSmoth)
        {
            elapsedTime += Time.deltaTime;
            _healthbar.fillAmount = Mathf.Lerp(startValue, endValue, elapsedTime / durationSmoth);

            yield return null;
        }

        _healthbar.fillAmount = endValue;
        _smoothChangeValue = null;
    }
}
