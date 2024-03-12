using UnityEngine;
using UnityEngine.UI;

public class HealthView : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private Slider _slider;

    private void OnEnable()
    {
        _player.HealthChange += OnHealthChanged;
        //добавить переменную
        _slider.value = 1;
    }

    private void OnDisable()
    {
        _player.HealthChange -= OnHealthChanged;
    }

    private void OnHealthChanged(int value, int maxValue)
    {
        _slider.value = (float)value / maxValue;
    }
}
