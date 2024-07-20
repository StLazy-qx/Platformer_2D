using System.Collections;
using UnityEngine;


public class VisualVampireEffect : MonoBehaviour
{
    [SerializeField] private VampireEffect _targetEffect;
    [SerializeField] private SpriteRenderer _spriteEffect;

    private Coroutine _changedAlphaChannel;

    private void Start()
    {
        Color initialColor = _spriteEffect.color;
        initialColor.a = 0f;
        _spriteEffect.color = initialColor;
    }

    private void OnEnable()
    {
        _targetEffect.UsingVampirism += OnUsingVampirism;
    }

    private void OnDisable()
    {
        _targetEffect.UsingVampirism -= OnUsingVampirism;
    }

    private void OnUsingVampirism()
    {
        if (_changedAlphaChannel != null)
        {
            StopCoroutine(_changedAlphaChannel);
        }

        _changedAlphaChannel = StartCoroutine(ChangeAlphaChannel());
    }

    private IEnumerator ChangeAlphaChannel()
    {
        _spriteEffect.enabled = true;
        float elapsedTime = 0;
        Color initialColor = _spriteEffect.color;
        initialColor.a = 1f;
        _spriteEffect.color = initialColor;

        while (elapsedTime < _targetEffect.TimeAction)
        {
            elapsedTime += Time.deltaTime;
            Color newColor = _spriteEffect.color;
            newColor.a = Mathf.Lerp(1f, 0f, elapsedTime / _targetEffect.TimeAction);
            _spriteEffect.color = newColor;

            yield return null;
        }

        Color finalColor = _spriteEffect.color;
        finalColor.a = 0f;
        _spriteEffect.color = finalColor;
    }
}
