using System.Collections;
using UnityEngine;
using TMPro;

public class CooldownVampireEffect : MonoBehaviour
{
    [SerializeField] private VampireEffect _targetEffect;
    [SerializeField] private TextMeshProUGUI _textTimeCooldown;

    private void Start()
    {
        _textTimeCooldown.text = "Vampirism Ready";
    }

    private void OnEnable()
    {
        _targetEffect.Cooldowning += OnCooldown;
    }

    private void OnDisable()
    {
        _targetEffect.Cooldowning -= OnCooldown;
    }

    private void OnCooldown()
    {
        if (_textTimeCooldown != null)
        {
            StartCoroutine(BeginCountdown());
        }
    }

    private IEnumerator BeginCountdown()
    {
        WaitForSeconds waitForSeconds = new WaitForSeconds(1f);
        float remainingTime = _targetEffect.TimeAction;

        while (remainingTime > 0)
        {
            _textTimeCooldown.text = "Vampirism: " + remainingTime.ToString("F0");

            yield return waitForSeconds;

            remainingTime --;
        }

        _targetEffect.EnableVampirism();

        _textTimeCooldown.text = "Vampirism Ready";
    }
}
