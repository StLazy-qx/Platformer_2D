using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BalanceCoin : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private TMP_Text _money;

    private void OnEnable()
    {
        _player._MoneyView += OnCoinView;
    }

    private void OnDisable()
    {
        _player._MoneyView -= OnCoinView;
    }

    private void OnCoinView(int value)
    {
        _money.text = value.ToString();
    }
}
