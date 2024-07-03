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

    private void Update()
    {
        _healthbar.transform.rotation = Quaternion.identity;
    }

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
