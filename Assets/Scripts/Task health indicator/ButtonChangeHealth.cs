using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonChangeHealth : MonoBehaviour
{
    [SerializeField] private Button _damageButton;
    [SerializeField] private Button _healthButton;
    [SerializeField] private PersonHealthSystem _target;

    private int damage = 10;
    private int heal = 10;

    private void Start()
    {
        _healthButton.onClick.AddListener(OnHealthButtonClick);
        _damageButton.onClick.AddListener(OnDamageButtonClick);
    }

    private void OnHealthButtonClick()
    {
        _target.TakeHeal(heal);
    }    
    
    private void OnDamageButtonClick()
    {
        _target.TakeDamage(damage);
    }
}
